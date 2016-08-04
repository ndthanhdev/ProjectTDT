using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.Common;
using TDTX.ViewModels;
using Xamarin.Forms;


namespace TDTX.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<SettingsPageViewModel>(this, "LanguageChanged",
                async (sender) =>
                {
                    await Task.WhenAll(DisplayAlert(TextProvider.Translate("Closing"),
                        TextProvider.Translate("ClosingMessage"),
                        TextProvider.Translate("OK")),
                        ((TDTX.App)App.Current).SaveSate());
#if UWP
                                Windows.UI.Xaml.Application.Current.Exit();
#endif

                });
        }
    }
}
