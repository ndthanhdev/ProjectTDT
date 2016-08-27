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
using XTDT.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace XTDT.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeTablePage : Page
    {
        public TimeTablePage()
        {
            this.InitializeComponent();
            //schedule.ManipulationStarted += Schedule_ManipulationStarted;
            //schedule.ManipulationCompleted += Schedule_ManipulationCompleted;
        }

        //private void Schedule_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    pivot.IsHitTestVisible = true;
        //}

        //private void Schedule_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        //{
        //    pivot.IsHitTestVisible = false;            
        //}

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await (DataContext as TimeTablePageViewModel).PrepareData();
        }

        private void ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
                (sender as ListView).SelectedItem = null;
        }

        private async void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            await (DataContext as TimeTablePageViewModel).UpdateAgenda(e.NewDate.Date);
        }
    }
}
