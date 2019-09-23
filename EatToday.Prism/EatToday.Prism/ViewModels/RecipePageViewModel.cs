using EatToday.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class RecipePageViewModel : ViewModelBase
    {
        private RecipeResponse _recipe;
        public RecipePageViewModel(
            INavigationService navigationService) : base(navigationService)
        {

        }

        public RecipeResponse Recipe
        {
            get => _recipe;
            set => SetProperty(ref _recipe, value);

        }
        public ICollection<IngredientRecipeResponse> Ingredients { get;  set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("recipe"))
            {
                Recipe = parameters.GetValue<RecipeResponse>("recipe");
                Title = Recipe.Name;
                Ingredients = Recipe.IngredientRecipes;
            }
        }
    }
}
