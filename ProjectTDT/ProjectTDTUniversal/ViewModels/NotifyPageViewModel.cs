using ProjectTDTUniversal.Models;
using ProjectTDTUniversal.Services.DataServices;
using ProjectTDTUniversal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Newtonsoft.Json;

namespace ProjectTDTUniversal.ViewModels
{
    
    public class NotifyPageViewModel : Mvvm.ViewModelBase,ILiveContent
    {
        private ObservableCollection<Notify> _notifys;

        
        public ObservableCollection<Notify> Notifys
        {
            get { return _notifys= _notifys ?? new ObservableCollection<Notify>(); }
            set { Set(ref _notifys, value); }
        } 
        public NotifyPageViewModel()
        {
        }

        public async override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Notifys.Clear();
            if (state.ContainsKey(nameof(Notifys)))
            {
                Notifys = (ObservableCollection<Notify>)state[nameof(Notifys)] ?? new ObservableCollection<Notify>();
            }
            else
            {
                var ie = await Transporter.Instance.GetNotify();
                foreach (var n in ie)
                {
                    Notifys.Add(n);
                }
            }
            ContentService.Refresh(this);
               
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
                state[nameof(Notifys)] = Notifys;
            await Task.Yield();
        }

        public void GotoPrivacy()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);
        }

        public void GotoAbout()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        }

        public Task UpdateContents()
        {
            throw new NotImplementedException();
        }

        public ICommand ItemSelected
        {
            get
            {
                return new Template10.Mvvm.DelegateCommand<Notify>((i) =>
                 {
                     NavigationService.Navigate(typeof(NotifyDetailPage), i);
                     
                 });
            }
            
        }

        public KeyValuePair<string, Type>[] Properties => new KeyValuePair<string, Type>[]
        {
            new KeyValuePair<string, Type>(nameof(Notifys),typeof(Notify))
        };
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
