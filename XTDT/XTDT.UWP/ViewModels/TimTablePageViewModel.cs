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
    public class TimTablePageViewModel : ViewModelBase
    {
        public TimTablePageViewModel()
        {
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
            await DataCotroller.UpdateHocKyDictionaryKey(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
            foreach (var thongTinHocKy in DataCotroller.HocKyDictionary.Keys)
                HocKyList.Add(thongTinHocKy);
            //TODO add new data to
            await DataCotroller.UpdateDictionaryValueAsync(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
            await SaveAndLoad.SaveTextAsync("TkbData.txt", JsonConvert.SerializeObject(DataCotroller));
        }

        public async Task LoadData()
        {
            var json = await SaveAndLoad.LoadTextAsync("TkbData.txt");
            DataCotroller = JsonConvert.DeserializeObject<TkbDataController>(json);
            RaisePropertyChanged(nameof(HocKyList));
        }
        public async Task PrepareData()
        {
            await LoadData();
            await UpdateAsync();
        }
        public ICommand SelectHocKyCommand => new DelegateCommand<ThongTinHocKy>( async (tthk) => await SelectHocKy(tthk));

        public async Task SelectHocKy(ThongTinHocKy tthk)
        {
            //TODO check busy
            await Task.Yield();

            //TODO clear
            if (tthk == null)
                return;
            if (!DataCotroller.HocKyDictionary.ContainsKey(tthk))
                return;
            if (DataCotroller.HocKyDictionary[tthk] == null)
                await DataCotroller.ProvideHocKyValue(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password, tthk);
            //TODO update display
        }

        //private ObservableCollection<TkbItem> _overallSunday;
        //private ObservableCollection<TkbItem> _overallMonday;
        //private ObservableCollection<TkbItem> _overallTuesday;
        //private ObservableCollection<TkbItem> _overallWednesday;
        //private ObservableCollection<TkbItem> _overallThursday;
        //private ObservableCollection<TkbItem> _overallFriday;
        //private ObservableCollection<TkbItem> _overallSaturday;
        //private int _selectedSemesterIndex;

        //public ObservableCollection<TkbItem> OverallSunday
        //{
        //    get { return _overallSunday ?? (_overallSunday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallSunday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallMonday
        //{
        //    get { return _overallMonday ?? (_overallMonday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallMonday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallTuesday
        //{
        //    get { return _overallTuesday ?? (_overallTuesday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallTuesday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallWednesday
        //{
        //    get { return _overallWednesday ?? (_overallWednesday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallWednesday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallThursday
        //{
        //    get { return _overallThursday ?? (_overallThursday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallThursday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallFriday
        //{
        //    get { return _overallFriday ?? (_overallFriday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallFriday = value; }
        //}

        //public ObservableCollection<TkbItem> OverallSaturday
        //{
        //    get { return _overallSaturday ?? (_overallSaturday = new ObservableCollection<TkbItem>()); }
        //    private set { _overallSaturday = value; }
        //}

    }
}
