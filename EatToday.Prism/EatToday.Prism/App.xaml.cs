using Prism;
using Prism.Ioc;
using EatToday.Prism.ViewModels;
using EatToday.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EatToday.Common.Services;
using Newtonsoft.Json;
using EatToday.Common.Models;
using EatToday.Common.Helpers;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EatToday.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY0MzYwQDMxMzcyZTMzMmUzMENmNTlSd1E0MmtLZ1I3RkVrMjF4ZXFXd1haR3lha2Z6RjNuZERhN3pSTVk9");
            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/EatTodayMasterDetailPage/NavigationPage/ChooseIngredientsPage");
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RecipesPage, RecipesPageViewModel>();
            containerRegistry.RegisterForNavigation<RecipePage, RecipePageViewModel>();
            containerRegistry.RegisterForNavigation<IngredientsRecipePage, IngredientsRecipePageViewModel>();
            containerRegistry.RegisterForNavigation<PreparationRecipePage, PreparationRecipePageViewModel>();
            containerRegistry.RegisterForNavigation<RecipeTabbedPage, RecipeTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<ChooseIngredientsPage, ChooseIngredientsPageViewModel>();
            containerRegistry.RegisterForNavigation<EatTodayMasterDetailPage, EatTodayMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
        }
    }
}
