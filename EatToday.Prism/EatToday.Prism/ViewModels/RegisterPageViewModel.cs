﻿using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Common.Services;
using EatToday.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _registerCommand;

        public RegisterPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Register new user";
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(Register));

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

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

        private async void Register()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new UserRequest
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Phone = Phone
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RegisterUserAsync(
                url,
                "api",
                "/Account",
                request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                "Accept");
            await _navigationService.GoBackAsync();

        }

        private async Task<bool> ValidateData()
        {

            if (string.IsNullOrEmpty(FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailValidError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordMinimumError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirmError, Languages.Accept);
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NotMatchPasswordError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
