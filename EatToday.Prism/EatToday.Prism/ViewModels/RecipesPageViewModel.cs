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
        private ObservableCollection<RecipeItemViewModel> _recipes;
        public RecipesPageViewModel( INavigationService  navigationService) : base(navigationService)
        {
            Title = "Recipes";
        }

        public ObservableCollection<RecipeItemViewModel> Recipes
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
                var _recipe2 = new [] { _recipe };
                Recipes = new ObservableCollection<RecipeItemViewModel>(_recipe2.Select(r => new RecipeItemViewModel
                {
                    Name = r.Name,
                    Description = r.Description,
                    Instructions = r.Instructions,
                    ImageUrl = r.ImageUrl,
                    RecipeType = r.RecipeType,
                    IngredientRecipes = r.IngredientRecipes,
                    CommentResponses = r.CommentResponses,
                }).ToList());
            }
            
        }
    }
}
