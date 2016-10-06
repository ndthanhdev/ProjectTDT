using Windows.UI.Xaml;
using System.Threading.Tasks;
using TDTUniversal.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using TDTUniversal.API.Requests;
using TDTUniversal.API;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
using System.Net;
using TDTUniversal.API.Respond;
using TDTUniversal.Services;
using TDTUniversal.DataContext;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.UI;

namespace TDTUniversal
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public static readonly string CACHE_FOLDER = "all_cache";
        public App()
        {
            InitializeComponent();

            //Task.Run(async () =>
            //{
            //    LocalDataService.Instance.StudentID = "51403318";
            //    LocalDataService.Instance.Password = "51403318TDT";
            //    var sv = await TokenService.GetTokenProvider().GetTokenAsync();
            //    var r = await ApiClient.GetAsync<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest("51403318"), TokenService.GetTokenProvider());
            //    await Task.Yield();
            //});

            SplashFactory = (e) => new Views.Splash(e);

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
            //await Task.Delay(500);
            if (LocalDataService.Instance.IsLogged)
            {
                var prepareDBTask = Task.Run(async () =>
                {
                    using (var database = new TDTContext())
                    {
                        await database.Database.EnsureCreatedAsync();
                    }
                });
                //initialize cache
                var cacheTask = FileCache.Instance.InitializeAsync(Windows.Storage.ApplicationData.Current.LocalCacheFolder, CACHE_FOLDER);                
                await Task.WhenAll(prepareDBTask, cacheTask);
                NavigationService.Navigate(typeof(Views.HomePage));
            }
            else
            {
                NavigationService.Navigate(typeof(Views.LoginPage));
            }
            await Task.CompletedTask;
        }
    }
}

