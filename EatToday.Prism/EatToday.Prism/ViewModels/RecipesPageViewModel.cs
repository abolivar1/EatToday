﻿using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Prism.Helpers;
using Newtonsoft.Json;
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
        private readonly INavigationService _navigationService;
        private ObservableCollection<RecipeItemViewModel> _recipes;
        public RecipesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Recipes;
            _navigationService = navigationService;
            LoadRecipes();
        }



        public ObservableCollection<RecipeItemViewModel> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }


        private void LoadRecipes()
        {
            var _recipes = JsonConvert.DeserializeObject<List<RecipeResponse>>(Settings.Recipes);

            var recipesDeuplicate = new ObservableCollection<RecipeItemViewModel>(_recipes.Select(r => new RecipeItemViewModel(_navigationService)
            {
                Name = r.Name,
                Description = r.Description,
                Instructions = r.Instructions,
                ImageUrl = r.ImageUrl,
                RecipeType = r.RecipeType,
                IngredientRecipes = r.IngredientRecipes,
                CommentResponses = r.CommentResponses,

            }).ToList());

            Recipes = new ObservableCollection<RecipeItemViewModel>();
            foreach (var item in recipesDeuplicate)
            {
                var found = Recipes.FirstOrDefault(r => r.Name == item.Name);
                if (found == null)
                {
                    Recipes.Add(item);
                }
            }
        }
    }
}
