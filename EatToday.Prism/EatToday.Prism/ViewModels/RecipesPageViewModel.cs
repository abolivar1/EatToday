using EatToday.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class RecipesPageViewModel : ViewModelBase
    {
        private  RecipeResponse _recipe;
        private ObservableCollection<RecipeResponse> _recipes;
        public RecipesPageViewModel( INavigationService  navigationService) : base(navigationService)
        {
            Title = "Recipes";
        }

        public ObservableCollection<RecipeResponse> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey("recipe"))
            {
                _recipe = parameters.GetValue<RecipeResponse>("recipe");
                //var _recipe2 = _recipe as IEnumerable<RecipeResponse>;
                //Recipes = new ObservableCollection<RecipeResponse>(_recipe);
            }
            
        }
    }
}
