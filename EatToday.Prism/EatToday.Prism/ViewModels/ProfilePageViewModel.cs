using EatToday.Common.Helpers;
using EatToday.Common.Models;
using EatToday.Common.Services;
using EatToday.Prism.Helpers;
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

        private async void Save()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Customer.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Customer.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Customer.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }
            if (string.IsNullOrEmpty(Customer.PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            return true;
        }



    }
}


