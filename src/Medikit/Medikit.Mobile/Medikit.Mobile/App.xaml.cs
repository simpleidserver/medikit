using Medikit.EHealth.KeyStore;
using Medikit.Mobile.Services;
using Medikit.Mobile.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Medikit.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEHealth();
            RegisterMedikitApplicationDependencies(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            MainPage = new AppShell();
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        
        protected override void OnStart()
        {
            AppCenter.Start("android=ac140158-0029-4e8b-bd73-13f8b7088304;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static void RegisterMedikitApplicationDependencies(IServiceCollection services)
        {
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<ICertificateStore, SqliteCertificateStore>();
            services.AddTransient<IKeyStoreManager, MobileKeyStoreManager>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<CertificatesViewModel>();
            services.AddTransient<PrescriptionsViewModel>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddOptions<MedikitMobileOptions>();
            services.AddHttpClient("apiClient");
        }
    }
}
