using EatToday.Common.Models;
using EatToday.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;

            //TODO: Delete this lines
            Email = "juanmartinez1712@gmail.com";
            Password = "123456";
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an password.", "Accept");
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            // Validando la conexión
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email or Password incorrect.", "Accept");
                Password = string.Empty;

                IsRunning = false;
                IsEnabled = true;
                return;

            }
            // TODO: Hacer esto en view model donde se escogen los ingredientes
            var token = response.Result;
            var response2 = await _apiService.GetRecipesByIngredientsAsync(url, "/api", "/Recipes/GetRecipes", "bearer", token.Token, 1 );
            if (!response2.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "We have a big problem, sorry", "Accept");
                Password = string.Empty;

                IsRunning = false;
                IsEnabled = true;
                return;

            }
            var recipe = response2.Result;

            var parameters = new NavigationParameters
            {
                { "recipe", recipe }
            };
            //TDO: Lo anterior se hizo para avanzar en la visualización.

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("RecipesPage", parameters);
            Password = string.Empty;

        }
    }
}
