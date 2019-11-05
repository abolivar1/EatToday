using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public ProfilePageViewModel (INavigationService navigationservice) : base (navigationservice)
        {
            Title = "Profile";
        }
    }
}
