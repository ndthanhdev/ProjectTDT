using ProjectTDTUniversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ProjectTDTUniversal.ViewModels
{
    public class NotifyDetailPageViewModel:Mvvm.ViewModelBase
    {
        private Notify _notify;

        public NotifyDetailPageViewModel()
        {
           
        }

        public Notify Notify
        {
            get
            {
                return _notify ?? new Notify();
            }
            private set
            {
                _notify = value;
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                Notify = (Notify)parameter;
            }
            catch
            {

            }
        }


    }
}
