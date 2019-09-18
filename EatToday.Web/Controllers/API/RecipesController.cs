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

            var recipe = await _dataContext.Recipes
                .Include(r => r.RecipeType)
                .Include(r => r.Comments)
                .Include(r => r.RateRecipes)
                .Include(r => r.FavouriteRecipes)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeRequest.Ingredients);
                //.All(r => r.RecipeIngredients.Count > 0);
                //.AnyAsync(r => r.RecipeIngredients.Count > 0);

                //.Any(r => r.RecipeIngredients.Any(ri => ri.Ingredient == ))

            var response = new RecipeResponse
            {

                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageFullPath,
                RecipeType = recipe.RecipeType.Name,
                IngredientRecipes = recipe.RecipeIngredients.Select(ri => new IngredientRecipeResponse
                {
                    Id = ri.Id,
                    Amount = ri.Amount,
                    Ingredient = ri.Ingredient.Name
                }).ToList(),
                CommentResponses = recipe.Comments.Select(c => new CommentResponse
                {
                    Id = c.Id,
                    Remarks = c.Remarks,
                    Date = c.DateLocal,
                    Customer = c.Customer.User.FullName
                }).ToList()

            };
            return Ok(response);
        }
    }
}
