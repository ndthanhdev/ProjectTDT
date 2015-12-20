using ProjectTDTWindows.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectTDTWindows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel _mainViewModel;
        public MainViewModel MainViewModel
        {
            set { _mainViewModel = value; }
            get
            {
                if (_mainViewModel == null) _mainViewModel = new MainViewModel();
                return _mainViewModel;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await MainViewModel.LoadData();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
          
            // for logout method
            if (e.SourcePageType == typeof(LoginPage))
            {
                this.Frame.BackStack.Clear();
            }

        }

       
    }
}
