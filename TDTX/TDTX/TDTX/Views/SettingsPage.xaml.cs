using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.ViewModels;
using Xamarin.Forms;


namespace TDTX.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            //MessagingCenter.Subscribe<SettingsPageViewModel,>();
        }

        private async void ChangeLanguage_OnClicked(object sender, EventArgs e)
        {
            SettingsPageViewModel.Instance.SelectLanguageCommand.Execute((sender as Button).CommandParameter);

        }

    }
}
