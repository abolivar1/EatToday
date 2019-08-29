using EatToday.Web.Data;
using EatToday.Web.Data.Entities;
using EatToday.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Recipe> ToRecipeAsync(RecipeViewModel model, string path)
        {
            return new Recipe
            {
                Comments = model.Comments,
                RateRecipes = model.RateRecipes,
                FavouriteRecipes = model.FavouriteRecipes,
                RecipeIngredients = model.RecipeIngredients,
                Id = model.Id,
                ImageUrl = path,
                Name = model.Name,
                RecipeType = await _dataContext.RecipeTypes.FindAsync(model.RecipeTypeId),
                Description = model.Description,
                Instructions = model.Instructions,
            };
        }

        public async Task<RecipeIngredient> ToIngredientAsync(IngredientViewModel model, bool isNew)
        {
            return new RecipeIngredient
            {
                Id = isNew ? 0 : model.Id,
                Amount = model.Amount,
                Ingredient = await _dataContext.Ingredients.FindAsync(model.IngredientId),
                Recipe = await _dataContext.Recipes.FindAsync(model.RecipeId),
            };
        }
    }
}
