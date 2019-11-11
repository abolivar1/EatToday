using EatToday.Common.Models;
using EatToday.Prism.ViewModels;
using MyVet.Prism.ViewModels;
using EatToday.Prism.Helpers;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EatToday.Prism.ViewModels
{
    public class EatTodayMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public EatTodayMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ProfilePage",
                    Title = Languages.Profile
                },

                new Menu
                {
                    Icon = "ic_search",
                    PageName = "ChooseIngredientsPage",
                    Title = Languages.SearchRecipes
                },

                new Menu
                {
                    Icon = "ic_near_me",
                    PageName = "MapPage",
                    Title = Languages.GetIngredients
                },

                new Menu
                {
                    Icon = "ic_exit_left",
                    PageName = "LoginPage",
                    Title = Languages.Logout
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
