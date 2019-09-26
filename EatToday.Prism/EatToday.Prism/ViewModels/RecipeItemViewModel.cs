using EatToday.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Prism.ViewModels
{
    public class RecipeItemViewModel : RecipeResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectRecipeCommand;
        public RecipeItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectRecipeCommand => _selectRecipeCommand ?? (_selectRecipeCommand = new DelegateCommand(SelectRecipe));

        private async void SelectRecipe()
        {
            var parameters = new NavigationParameters
            {
                { "recipe", this }
            };

            await _navigationService.NavigateAsync("IngredientsRecipePage", parameters);
        }
    }
}
