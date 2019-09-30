using EatToday.Common.Helpers;
using EatToday.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class IngredientsRecipePageViewModel : ViewModelBase
    {
        private RecipeResponse _recipe;

        public IngredientsRecipePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Recipe = JsonConvert.DeserializeObject<RecipeResponse>(Settings.Recipe);
            Title = "Ingredients";
        }

        public RecipeResponse Recipe
        {
            get => _recipe;
            set => SetProperty(ref _recipe, value);
        }
    }
}
