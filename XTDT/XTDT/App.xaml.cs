using Windows.UI.Xaml;
using System.Threading.Tasks;
using XTDT.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using XTDT.Views;
using XTDT.Services.LocalDataServices;
using TDTUniversal.DataContext;
using Microsoft.Data.Entity;

namespace XTDT
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            // Before running the app for the first time, follow these steps:
            // 1- Build -> Build the Project
            // 2- Tools –> NuGet Package Manager –> Package Manager Console
            // 3- Run "Add-Migration MyFirstMigration" to scaffold a migration to create the initial set of tables for your model
            // See here for more information https://docs.efproject.net/en/latest/platforms/uwp/getting-started.html#create-your-database

            using (var database = new TDTContext())
            {
                database.Database.Migrate();
            }           

            //Read more at https://blogs.windows.com/buildingapps/2016/05/03/data-access-in-universal-windows-platform-uwp-apps/#6ma6lWSGcKGtTfTo.99

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (Window.Current.Content as ModalDialog == null)
            {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);

                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy(),
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here
            await Task.Delay(5000);

            if (LocalDataService.Instance.IsLogged)
            {
                NavigationService.Navigate(typeof(HomePage));
            }
            else
            {
                NavigationService.Navigate(typeof(LoginPage));
            }
            await Task.CompletedTask;
        }
    }
}

