using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using Syncfusion.SfAutoComplete.XForms;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace EatToday.Prism.Views
{
    public partial class ChooseIngredientsPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        List<string> Ingredients = new List<string>();
        private bool _isRunning;
        private bool _isEnabled;
        public ChooseIngredientsPage(INavigationService navigationService ,IApiService apiService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _apiService = apiService;

        }

        public bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }

        public bool IsEnabled2
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        private void autoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            var addedIngredient = e.AddedItems as IngredientResponse;
            var removeIngredient = e.RemovedItems as IngredientResponse;

            if (addedIngredient != null)
            {
                Ingredients.Add(addedIngredient.Name);

            }
            if (removeIngredient != null)
            {
                Ingredients.Remove(removeIngredient.Name);

            }            
        }
        
        private async void searchRecipes(object sender, EventArgs args)
        {


            //await DisplayAlert("Error", "You must enter an email." + Ingredients[0] , "Accept");

            if (Ingredients.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an ingredient.", "Accept");
                return;
            }

            //IsRunning = true;
            IsEnabled2 = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled2 = true;
                //IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            //var response2 = await _apiService.GetRecipesByIngredientsAsync(url, "/api", "/Recipes/GetRecipes", "bearer", token.Token, Ingredients);
            //if (!response2.IsSuccess)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "We have a big problem, sorry", "Accept");

            //    IsRunning = false;
            //    IsEnabled = true;
            //    return;

            //}
            //var recipe = response2.Result;

            //var parameters = new NavigationParameters
            //{
            //    { "recipe", recipe }
            //};

            //IsRunning = false;
            //IsEnabled = true;

            //await _navigationService.NavigateAsync("RecipesPage", parameters);
        }
    }
}
