using ProjectTDTUniversal.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProjectTDTUniversal.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            
            //test zone

           // ProjectTDTUniversal.Services.DataServices.CredentialsServices.SetCredential("","1");
        }

        // strongly-typed view models enable x:bind
        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;
    }
}
