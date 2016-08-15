using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.API.Respond;
using XTDT.DataController;

namespace XTDT.UWP.ViewModels
{
    public class TimTablePageViewModel : ViewModelBase
    {
        private TkbDataController _dataController;
        public TimTablePageViewModel()
        {
            _dataController = TkbDataController.Instance;
        }
        private ObservableCollection<ThongTinHocKy> _hocKyList;
        public ObservableCollection<ThongTinHocKy> HocKyList
        {
            get { return _hocKyList ?? (_hocKyList = new ObservableCollection<ThongTinHocKy>(_dataController.HocKyDictionary.Keys)); }
        }
        public ICommand UpdateCommand => new AwaitableDelegateCommand(Update);

        public Task Update(AwaitableDelegateCommandParameter arg)
        {
            Task.Yield();

            return Task.CompletedTask;
        }
    }
}
