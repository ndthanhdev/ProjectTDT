using ProjectTDTUniversal.Models;
using ProjectTDTUniversal.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ProjectTDTUniversal.ViewModels
{
    public class NotifyDetailPageViewModel:Mvvm.ViewModelBase,INotifyPropertyChanged
    {
        public NotifyDetailPageViewModel()
        {
           
        }

        private Notify _notify;

        public Notify Notify
        {
            get { return _notify ?? new Notify(); }
            private set {  Set(ref _notify, value);  }
        } 

        public NotifyDetail _detail;

        public NotifyDetail Detail
        {
            get { return _detail ?? new NotifyDetail("", new ObservableCollection<KeyValuePair<string, Uri>>()); }
            set { Set(ref _detail, value); }
        }


        public override async void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                Notify = (Notify)parameter;
                Detail = await Transporter.Instance.GetNotifyContent(Notify.Link);                
            }
            catch
            {

            }
        }

        public void GotoPrivacy()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);
        }

        public void GotoAbout()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        }

    }
}
