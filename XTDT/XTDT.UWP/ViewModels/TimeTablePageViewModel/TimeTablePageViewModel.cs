using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        }
        private static TkbDataController _dataController;
        public TkbDataController DataCotroller
        {
            get
            {
                return _dataController ?? (_dataController = new TkbDataController());
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
            try
            {
                Quest++;
                await Task.Yield();
                if (await DataCotroller.UpdateHocKyDictionaryKey(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password))
                {
                    await UpdateOverallKey();
                }
                // add new data to
                await DataCotroller.UpdateDictionaryValueAsync(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
                await SaveAndLoad.SaveTextAsync("TkbData.txt", JsonConvert.SerializeObject(DataCotroller, Formatting.Indented));
                await UpdateAgenda(SelectedDate.Date);
                await InitializeCalendar();
                await UpdateOverallValue(SelectedTTHK);
            }
            catch { }
            finally
            {
                Quest--;
            }
        }

        public async Task LoadData()
        {
            try
            {
                Quest++;
                var json = await SaveAndLoad.LoadTextAsync("TkbData.txt");
                DataCotroller = JsonConvert.DeserializeObject<TkbDataController>(json);
                await UpdateAgenda(SelectedDate.Date);
                await InitializeCalendar();
                await UpdateOverallKey();
                await UpdateOverallValue(SelectedTTHK);
            }
            catch { }
            finally
            {
                Quest--;
            }
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

        private TimeSpan ThuToBonusDay(int thu)
        {
            return TimeSpan.FromDays(thu > 1 ? thu - 2 : 6);
        }

        private int _quest = 0;
        public int Quest
        {
            get { return _quest; }
            set
            {
                if (value >= 0)
                {
                    Set(ref _quest, value);
                }
            }
        }
    }
}
