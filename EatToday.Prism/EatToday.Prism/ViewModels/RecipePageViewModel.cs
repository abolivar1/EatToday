using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Prism.Helpers;
using Newtonsoft.Json;
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

            Recipe = JsonConvert.DeserializeObject<RecipeResponse>(Settings.Recipe);
            Title = Languages.Recipe;
        }
    }
}
