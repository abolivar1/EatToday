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
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
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

        public RecipeViewModel ToRecipeViewModel(Recipe recipe)
        {
            return new RecipeViewModel
            {
                Comments = recipe.Comments,
                RateRecipes = recipe.RateRecipes,
                FavouriteRecipes = recipe.FavouriteRecipes,
                RecipeIngredients = recipe.RecipeIngredients,
                Id = recipe.Id,
                ImageUrl = recipe.ImageUrl,
                Name = recipe.Name,
                RecipeType = recipe.RecipeType,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                RecipeTypeId = recipe.RecipeType.Id,
                RecipeTypes = _combosHelper.GetComboRecipeTypes(),
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

        public IngredientViewModel ToIngredientViewModel(RecipeIngredient ingredient)
        {
            return new IngredientViewModel
            {

                Id = ingredient.Id,
                Amount = ingredient.Amount,
                Ingredient = ingredient.Ingredient,
                Recipe = ingredient.Recipe,
                Ingredients = _combosHelper.GetComboIngredients(),
                IngredientId = ingredient.Ingredient.Id,
                RecipeId = ingredient.Recipe.Id,

            };
        }
    }
}
