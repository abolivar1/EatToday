using Prism;
using Prism.Ioc;
using EatToday.Prism.ViewModels;
using EatToday.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EatToday.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EatToday.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RecipesPage, RecipesPageViewModel>();
        }
    }
}
