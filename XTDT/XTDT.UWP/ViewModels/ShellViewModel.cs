using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace XTDT.UWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        Services.LocalDataServices.LocalDataService _localData;
        public ShellViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _localData = Services.LocalDataServices.LocalDataService.Instance;
            }
        }
        public bool IsLogged
        {
            get { return _localData.IsLogged; }
            set { _localData.IsLogged = value; RaisePropertyChanged(); }
        }

    }
}
