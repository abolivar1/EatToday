using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EatToday.Web.Data;
using EatToday.Web.Data.Entities;
using EatToday.Web.Models;
using EatToday.Web.Helpers;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EatToday.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public RecipesController(
            DataContext context,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        // GET: Recipes
        public IActionResult Index()
        {
            return View(_context.Recipes
                .Include(rt => rt.RecipeType)
                .Include(c => c.Comments)
                .Include(r => r.RateRecipes)
                .Include(f => f.FavouriteRecipes)
                .Include(ri => ri.RecipeIngredients)
                .ThenInclude(i => i.Ingredient));
            // TODO: sacar los ingredientes
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(rt => rt.RecipeType)
                .Include(c => c.Comments)
                .Include(r => r.RateRecipes)
                .Include(f => f.FavouriteRecipes)
                .Include(ri => ri.RecipeIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public async  Task<IActionResult> Create()
        {

            var model = new RecipeViewModel
            {
                RecipeTypes = _combosHelper.GetComboRecipeTypes(),
            };
            return View(model);
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);

                }

                var recipe =  await _converterHelper.ToRecipeAsync(model, path);
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            model.RecipeTypes = _combosHelper.GetComboRecipeTypes();
            return View(model);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(rt => rt.RecipeType)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToRecipeViewModel(recipe));
        }

        //GET: Edit Ingredient on one recipe
        public async Task<IActionResult> EditIngredient(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RecipeIngredients = await _context.RecipeIngredients
                .Include(r => r.Recipe)
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (RecipeIngredients == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToIngredientViewModel(RecipeIngredients));
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeViewModel model)
        {

            if (ModelState.IsValid)
            {

                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);

                }

                var recipe = await _converterHelper.ToRecipeAsync(model, path);
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            model.RecipeTypes = _combosHelper.GetComboRecipeTypes();
            return View(model);
        }

        // POST: Edit Ingredient one recipe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIngredient(IngredientViewModel model)
        {

            if (ModelState.IsValid)
            {

                var recipeIngredient = await _converterHelper.ToIngredientAsync(model, false);
                _context.RecipeIngredients.Update(recipeIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.Id}");
            }


            model.Ingredients = _combosHelper.GetComboIngredients();
            return View(model);
        }

        public async Task<IActionResult> DeleteIngredient(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(h => h.Ingredient)
                .Include(h => h.Recipe)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{recipeIngredient.Recipe.Id}");
        }

        public async Task<IActionResult> DeleteRecipe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Comments)
                .Include(r => r.RateRecipes)
                .Include(r => r.FavouriteRecipes)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.RecipeIngredients.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "The recipe can't be deleted because it has related records. ");
                return RedirectToAction(nameof(Index));
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET: Add ingredients
        [HttpGet]
        public async Task<IActionResult> AddIngredient(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            var model = new IngredientViewModel
            {
                RecipeId = recipe.Id,
                Ingredients = _combosHelper.GetComboIngredients()
            };

            return View(model);
        }
        //POST: Add ingredients
        [HttpPost]
        public async Task<IActionResult> AddIngredient(IngredientViewModel model)
        {
            if(ModelState.IsValid)
            {
                var ingredient = await _converterHelper.ToIngredientAsync(model, true);
                _context.RecipeIngredients.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddIngredient));
            }

            model.Ingredients = _combosHelper.GetComboIngredients();
            return View(model);
        }
    }
}
