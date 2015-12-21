using ProjectTDTUniversal.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ProjectTDTUniversal.ViewModels
{
    public class NotifyPageViewModel : Mvvm.ViewModelBase
    {
        public NotifyPageViewModel()
        {

        }

        public async override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await Transporter.Instance.GetNotify();
        }

        //public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        //{
        //}
    }
}
