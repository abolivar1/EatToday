using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class ChooseIngredientsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ChooseIngredientsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Choose Ingredients";
        }
    }
}
