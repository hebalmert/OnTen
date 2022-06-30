using OnTen.Common.Services;
using OnTen.Prism.ViewModels;
using OnTen.Prism.Views;
using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace OnTen.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            //Para usar Syncfusion.
            SyncfusionLicenseProvider.RegisterLicense("NjU5NDQ5QDMyMzAyZTMxMmUzMGx5UzIvNW5zbTZrYmxsR2YyL0lmZjljbkFkTGhtb0NRV3FFQkdTWDJzRDA9");

            InitializeComponent();

            await NavigationService
                .NavigateAsync($"{nameof(OnTenMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            //Injection IApiService List <T>
            containerRegistry.Register<IApiService, ApiService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductsPage, ProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailPage, ProductDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowCarPage, ShowCarPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowHistoryPage, ShowHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<OnTenMasterDetailPage, OnTenMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<QualificationsPage, QualificationsPageViewModel>();
            containerRegistry.RegisterForNavigation<QualificationDetailPage, QualificationDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductTabbedPage, ProductTabbedPageViewModel>();
        }
    }
}
