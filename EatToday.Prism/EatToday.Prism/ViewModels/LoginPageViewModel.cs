using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Common.Services;
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
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isRemember;
        private DelegateCommand _forgotPasswordCommand;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;


        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            IsRemember = true;
            _navigationService = navigationService;
            _apiService = apiService;

            //TODO: Delete this lines
            Email = "juanmartinez1712@gmail.com";
            Password = "123456";
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));
        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(Register));

        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPassword));

        
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
        public bool IsRemember
        {
            get => _isRemember;
            set => SetProperty(ref _isRemember, value);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);
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
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckInternet, Languages.Accept);
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
            Settings.Token = JsonConvert.SerializeObject(response.Result);

            var token = response.Result;
            
            //Consumiendo el serivicio de todos los ingredients
            var response3 = await _apiService.GetIngredientsAsync(url, "/api", "/Recipes/GetIngredients", "bearer", token.Token);
            if (!response3.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "We have a big problem, sorry", "Accept");
                Password = string.Empty;

                IsRunning = false;
                IsEnabled = true;
                return;

            }
            var ingredient = response3.ResultList;

            Settings.Ingredients = JsonConvert.SerializeObject(response3.ResultList);
            Settings.IsRemembered = IsRemember;
            var requestEmail = new EmailRequest
            {
                Email = Email
            };
            var response4 = await _apiService.GetCustomerByEmailAsync(url, "/api", "/Customers/GetCustomerByEmail", "bearer", token.Token, requestEmail);
            if (!response4.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "We have a big problem, sorry", "Accept");

                IsRunning = false;
                IsEnabled = true;
                return;

            }

            Settings.Customer = JsonConvert.SerializeObject(response4.Result);
            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/EatTodayMasterDetailPage/NavigationPage/ChooseIngredientsPage");
            Password = string.Empty;

        }

        private async void Register()
        {
            await _navigationService.NavigateAsync("RegisterPage");
        }

        private async void ForgotPassword()
        {
            await _navigationService.NavigateAsync("RememberPasswordPage");
        }

    }
}
