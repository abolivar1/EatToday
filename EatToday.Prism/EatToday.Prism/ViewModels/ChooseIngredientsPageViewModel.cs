using EatToday.Common.Helpers;
using EatToday.Common.Models;
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
    public class ChooseIngredientsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<IngredientResponse> _ingredients;
        private string _itemSelected;
        //private List<string> _itemsSelected;
        public ChooseIngredientsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Choose Ingredients";
            LoadIngredients();
        }

        public string ItemsSelected
        {

            get => _itemSelected;
            set
            {
                _itemSelected = value;
            }
        }

        

        public ObservableCollection<IngredientResponse> IngredientsCollection
        {
            get => _ingredients;
            set => SetProperty(ref _ingredients, value);
        }

        

        private void LoadIngredients()
        {
            var _ingredients = JsonConvert.DeserializeObject<List<IngredientResponse>>(Settings.Ingredients);

            IngredientsCollection = new ObservableCollection<IngredientResponse>(_ingredients.Select(r => new IngredientResponse()
            {
                Id = r.Id,
                Name = r.Name,

            }).ToList());
        }
    }
}

