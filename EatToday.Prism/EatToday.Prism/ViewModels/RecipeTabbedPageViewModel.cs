using EatToday.Common.Helpers;
using EatToday.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class RecipeTabbedPageViewModel : ViewModelBase
    {
        public RecipeTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var recipe = JsonConvert.DeserializeObject<RecipeResponse>(Settings.Recipe);
            Title = recipe.Name;
        }
    }
}
