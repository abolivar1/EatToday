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

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey("ingredient"))
            {
                var ingredients = parameters.GetValue<List<IngredientResponse>>("ingredient");

                IngredientsCollection = new ObservableCollection<IngredientResponse>(ingredients.Select(r => new IngredientResponse()
                {
                    Id = r.Id,
                    Name = r.Name,

                }).ToList());
            }            
        }

        //private void AddItems(string value)
        //{
        //    _itemsSelected.Add(value);
        //    Console.WriteLine(value);
        //}
    }
}

