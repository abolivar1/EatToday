using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace EatToday.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private CustomerResponse _customer;
        private DelegateCommand _saveCommand;
        private DelegateCommand _changePasswordCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public ProfilePageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsRunning = false;
            IsEnabled = true;
            Customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePassword));
        public CustomerResponse Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
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
        private async void ChangePassword()
        {
            await _navigationService.NavigateAsync("ChangePasswordPage");
        }

        private async void Save()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var userRequest = new UserRequest
            {
                Address = Customer.Address,
                Email = Customer.Email,
                FirstName = Customer.FirstName,
                LastName = Customer.LastName,
                Password = "123456", // It doesn't matter what is sent here. It is only for the model to be valid
                Phone = Customer.PhoneNumber
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(
                url,
                "/api",
                "/Account",
                userRequest,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "We have a big problem, sorry",
                    "Accept");
                return;
            }

            Settings.Customer = JsonConvert.SerializeObject(Customer);

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                "User updated succesfully!",
                "Accept");

        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Customer.FirstName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a firstname", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(Customer.LastName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a lastname", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(Customer.Address))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Address", "Accept");
                return false;
            }
            if (string.IsNullOrEmpty(Customer.PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a PhoneNumber", "Accept");
                return false;
            }

            return true;
        }



    }
}


