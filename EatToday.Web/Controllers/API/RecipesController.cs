using EatToday.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EatToday.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EatToday.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecipesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RecipesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetRecipes")]
        //IngredientsRequest ingredientsRequest
        public async Task<IActionResult> GetRecipesAsync(RecipeRequest recipeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // TODO: Consulta de acuerdo a los ingredientes que escoga la persona, 
            // por el momento se traeran todas la recetas disponibles

            //var recipe = await _dataContext.Recipes
            //    .Include(r => r.RecipeType)
            //    .Include(r => r.Comments)
            //    .Include(r => r.RateRecipes)
            //    .Include(r => r.FavouriteRecipes)
            //    .Include(r => r.RecipeIngredients)
            //    .ThenInclude(r => r.Ingredient)
            //    .FirstOrDefaultAsync(r => r.RecipeIngredients.Count > 0);
            //.ToList();
            //.All(r => r.RecipeIngredients.Count > 0);
            //.AnyAsync(r => r.RecipeIngredients.Count > 0);

            //.Any(r => r.RecipeIngredients.Any(ri => ri.Ingredient == ))
            var recipes = await _dataContext.RecipeIngredients
                .Include(r => r.Recipe)
                .ThenInclude(r => r.RecipeType)
                .Include(r => r.Recipe)
                .ThenInclude(r => r.Comments)
                .Include(r => r.Recipe)
                .ThenInclude(r => r.RateRecipes)
                .Include(r => r.Recipe)
                .ThenInclude(r => r.FavouriteRecipes)
                .Include(r => r.Ingredient)
                .Where(r => r.Ingredient.Name == recipeRequest.Ingredients)
                .OrderBy(r => r.Recipe.Name)
                .ToListAsync();


            var response = new List<RecipeResponse>();

            foreach (var recipe in recipes)
            {
                var recipeResponse = new RecipeResponse
                {

                    Id = recipe.Recipe.Id,
                    Name = recipe.Recipe.Name,
                    Description = recipe.Recipe.Description,
                    Instructions = recipe.Recipe.Instructions,
                    ImageUrl = recipe.Recipe.ImageFullPath,
                    RecipeType = recipe.Recipe.RecipeType.Name,
                    IngredientRecipes = recipe.Recipe.RecipeIngredients.Select(ri => new IngredientRecipeResponse
                    {
                        Id = ri.Id,
                        Amount = ri.Amount,
                        Ingredient = ri.Ingredient.Name
                    }).ToList(),
                    CommentResponses = recipe.Recipe.Comments.Select(c => new CommentResponse
                    {
                        Id = c.Id,
                        Remarks = c.Remarks,
                        Date = c.DateLocal,
                        Customer = c.Customer.User.FullName
                    }).ToList()
                };
                response.Add(recipeResponse);
                
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("GetIngredients")]
        //IngredientsRequest ingredientsRequest
        public IActionResult GetIngredients()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var ingredients = _dataContext.Ingredients
                .Where(i => i.Id >= 0)
                .OrderBy(i => i.Name)
                .ToList();

            if (ingredients == null)
            {
                return NotFound();
            }

            return Ok(ingredients);
        }
    }
}
