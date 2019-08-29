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

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Instructions,ImageUrl")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
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
