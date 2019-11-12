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
using System.Threading.Tasks;

namespace EatToday.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _changePasswordCommand;

        public ChangePasswordPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = Languages.ChangePassword;
        }

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePassword));

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

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

        private async void ChangePassword()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new ChangePasswordRequest
            {
                Email = customer.Email,
                NewPassword = NewPassword,
                OldPassword = CurrentPassword
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Ok,
                response.Message,
                Languages.Accept);

            await _navigationService.GoBackAsync();

        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CurrentPasswordError,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordMinimumError,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordConfirmError,
                    Languages.Accept);
                return false;
            }

            if (!NewPassword.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NotMatchPasswordError,
                    Languages.Accept);
                return false;
            }

            return true;
        }
    }

}
