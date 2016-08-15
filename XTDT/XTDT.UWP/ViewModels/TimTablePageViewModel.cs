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
using XTDT.UWP.Services.LocalDataServices;

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
            get { return _hocKyList ?? (_hocKyList = new ObservableCollection<ThongTinHocKy>(_dataController.HocKyDictionary.Keys));}
            set { Set(ref _hocKyList, value); }
        }
        public ICommand UpdateCommand => new AwaitableDelegateCommand(UpdateAsync);

        public async Task UpdateAsync(AwaitableDelegateCommandParameter arg)
        {
            await Task.Yield();
            await _dataController.UpdateAsync(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
            foreach (var thongTinHocKy in _dataController.HocKyDictionary.Keys)
                HocKyList.Add(thongTinHocKy);
        }
    }
}
