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

namespace TDTUniversal
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {

            InitializeComponent();

            Task.Run(async () =>
            {
                TokenProvider tp = new TokenProvider("51403318", "51403318TDT");
                DSHocKyRequest dshkr = new DSHocKyRequest("51403318");
                var url = await RequestBuilder.BuildUrl(dshkr, tp);
                HttpClientHandler handler = new HttpClientHandler();
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                HttpClient client = new HttpClient(handler);
                // client.DefaultRequestHeaders.AcceptCharset.ParseAdd("utf-8");
                var s = await client.GetStringAsync(new Uri(url));
                var ss = WebUtility.HtmlDecode(s);

                await Task.Yield();
            });


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
            await Task.Delay(5000);

            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }
    }
}

