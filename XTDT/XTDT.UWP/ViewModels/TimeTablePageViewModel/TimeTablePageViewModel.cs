using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.API.Respond;
using XTDT.DataController;
using XTDT.Models;
using XTDT.UWP.Base.Local;
using XTDT.UWP.Common;
using XTDT.UWP.Services.LocalDataServices;

namespace XTDT.UWP.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        public TimeTablePageViewModel()
        {
            Schedule.Add(new Syncfusion.UI.Xaml.Schedule.ScheduleAppointment()
            {
                StartTime = DateTime.Parse("08/20/2016"),
                EndTime = DateTime.Parse("08/21/2016"),
                Subject="test appointment"
            });
        }
        private static TkbDataController _dataController;
        public TkbDataController DataCotroller
        {
            get
            {
                return _dataController ?? new TkbDataController();
            }
            set { _dataController = value; }

        }

        private ObservableCollection<ThongTinHocKy> _hocKyList;
        public ObservableCollection<ThongTinHocKy> HocKyList
        {
            get { return _hocKyList ?? (_hocKyList = new ObservableCollection<ThongTinHocKy>(DataCotroller.HocKyDictionary.Keys)); }
            set { Set(ref _hocKyList, value); }
        }
        public ICommand UpdateCommand => new DelegateCommand(async () => await UpdateAsync());


        public async Task UpdateAsync()
        {
            await Task.Yield();
            if (await DataCotroller.UpdateHocKyDictionaryKey(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password))
            {
                await UpdateOverallKey();
            }
            // add new data to
            await DataCotroller.UpdateDictionaryValueAsync(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
            await SaveAndLoad.SaveTextAsync("TkbData.txt", JsonConvert.SerializeObject(DataCotroller));
            await UpdateOverallValue(SelectedTTHK);
        }

        public async Task LoadData()
        {
            var json = await SaveAndLoad.LoadTextAsync("TkbData.txt");
            DataCotroller = JsonConvert.DeserializeObject<TkbDataController>(json);
            await UpdateOverallKey();
            await UpdateOverallValue(SelectedTTHK);
        }
        public async Task PrepareData()
        {
            await LoadData();
            await UpdateAsync();
        }

        public async Task<bool> ProvideHocKyValue(ThongTinHocKy tthk)
        {
            var result = await DataCotroller.ProvideHocKyValue(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password, tthk);
            await SaveAndLoad.SaveTextAsync("TkbData.txt", JsonConvert.SerializeObject(DataCotroller));
            return result;
        }

    }
}
