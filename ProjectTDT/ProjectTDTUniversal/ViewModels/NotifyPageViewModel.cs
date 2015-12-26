using ProjectTDTUniversal.Models;
using ProjectTDTUniversal.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace ProjectTDTUniversal.ViewModels
{
    public class NotifyPageViewModel : Mvvm.ViewModelBase,INotifyPropertyChanged
    {
        private ObservableCollection<Notify> _notifys;
        public ObservableCollection<Notify> Notifys { get; set; } = new ObservableCollection<Notify>();
        public NotifyPageViewModel()
        {
            Notifys.Add(new Notify());
        }

        public async override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await Transporter.Instance.GetNotifyContent(null);
            Notifys.Clear();
            if (state.ContainsKey(nameof(Notifys)))
            {
                Notifys = (ObservableCollection<Notify>)state[nameof(Notifys)] ?? new ObservableCollection<Notify>();
            }
            else
                foreach (var n in await Transporter.Instance.GetNotify())
                {
                    Notifys.Add(n);
                }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
                state[nameof(Notifys)] = Notifys;
            await Task.Yield();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        public ICommand ItemSelected
        {
            get
            {
                return new Common.RelayCommandEx<Notify>((i) =>                {
                   
                    Task.Run(()=> Windows.System.Launcher.LaunchUriAsync(i.Link));
                    
                });
            }
        }

    }
}
