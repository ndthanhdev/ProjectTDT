using ProjectTDTUniversal.Models;
using ProjectTDTUniversal.Services.DataServices;
using System;
using System.Collections.Generic;
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

        public Notify Notify { get; private set; } = new Notify();

        public NotifyDetail _detail;

        public NotifyDetail Detail
        {
            get { return _detail ?? new NotifyDetail("", new Dictionary<string, Uri>()); }
            set { Set(ref _detail, value); }
        }// = new NotifyDetail(null, new Dictionary<string, Uri>());


        public override async void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                Notify = (Notify)parameter;
                Detail = new NotifyDetail("...", new Dictionary<string, Uri>());
                Detail = await Transporter.Instance.GetNotifyContent(Notify.Link);
               // RaisePropertyChanged("Detail");

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
