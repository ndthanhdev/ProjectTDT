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
            if (await DataCotroller.UpdateHocKyDictionaryKey(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password))
            {
                //TODO for prevent from reset selected
                var currentSelected = SelectedTTHK;
                HocKyList.Clear();
                foreach (var thongTinHocKy in DataCotroller.HocKyDictionary.Keys)
                    HocKyList.Add(thongTinHocKy);
                SelectedTTHK = currentSelected;
            }
            // add new data to
            await DataCotroller.UpdateDictionaryValueAsync(LocalDataService.Instance.StudentID, LocalDataService.Instance.Password);
            await SaveAndLoad.SaveTextAsync("TkbData.txt", JsonConvert.SerializeObject(DataCotroller));
        }

        public async Task LoadData()
        {
            var json = await SaveAndLoad.LoadTextAsync("TkbData.txt");
            DataCotroller = JsonConvert.DeserializeObject<TkbDataController>(json);
            foreach (var thongTinHocKy in DataCotroller.HocKyDictionary.Keys)
                HocKyList.Add(thongTinHocKy);
            if (HocKyList.Count > 0)
                SelectedTTHK = HocKyList[0];
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

        public ICommand SelectHocKyCommand => new DelegateCommand<ThongTinHocKy>(async (tthk) => await SelectHocKy(tthk));

        public async Task SelectHocKy(ThongTinHocKy tthk)
        {
            //TODO check busy
            await Task.Yield();
            //TODO clear
            ClearOverallProperty();

            if (tthk == null)
                return;
            if (!DataCotroller.HocKyDictionary.ContainsKey(tthk))
                return;
            if (DataCotroller.HocKyDictionary[tthk] == null)
                //TODO save data here
                if (!await ProvideHocKyValue(tthk))
                    return;
            //TODO comfirm
            if (tthk != SelectedTTHK)
                return;
            if (DataCotroller.HocKyDictionary[tthk].Tkb == null)
                return;
            //TODO update display
            var hk = DataCotroller.HocKyDictionary[tthk];
            foreach (var tkb in hk.Tkb)
            {
                foreach (var lich in tkb.Lich)
                {
                    TkbItem tti = new TkbItem() { Tkb = tkb, Lich = lich };
                    switch (lich.Thu)
                    {
                        case 2:
                            OverallMonday.AddToOrdered(tti);
                            break;
                        case 3:
                            OverallTuesday.AddToOrdered(tti);
                            break;
                        case 4:
                            OverallWednesday.AddToOrdered(tti);
                            break;
                        case 5:
                            OverallThursday.AddToOrdered(tti);
                            break;
                        case 6:
                            OverallFriday.AddToOrdered(tti);
                            break;
                        case 7:
                            OverallSaturday.AddToOrdered(tti);
                            break;
                        default:
                            OverallSunday.AddToOrdered(tti);
                            break;
                    }
                }
            }
        }

        private ThongTinHocKy _selectedTTHK;
        public ThongTinHocKy SelectedTTHK
        {
            get { return _selectedTTHK; }
            set { Set(ref _selectedTTHK, value); }
        }

        private ObservableCollection<TkbItem> _overallSunday;
        private ObservableCollection<TkbItem> _overallMonday;
        private ObservableCollection<TkbItem> _overallTuesday;
        private ObservableCollection<TkbItem> _overallWednesday;
        private ObservableCollection<TkbItem> _overallThursday;
        private ObservableCollection<TkbItem> _overallFriday;
        private ObservableCollection<TkbItem> _overallSaturday;

        public ObservableCollection<TkbItem> OverallSunday
        {
            get { return _overallSunday ?? (_overallSunday = new ObservableCollection<TkbItem>()); }
            private set { _overallSunday = value; }
        }

        public ObservableCollection<TkbItem> OverallMonday
        {
            get { return _overallMonday ?? (_overallMonday = new ObservableCollection<TkbItem>()); }
            private set { _overallMonday = value; }
        }

        public ObservableCollection<TkbItem> OverallTuesday
        {
            get { return _overallTuesday ?? (_overallTuesday = new ObservableCollection<TkbItem>()); }
            private set { _overallTuesday = value; }
        }

        public ObservableCollection<TkbItem> OverallWednesday
        {
            get { return _overallWednesday ?? (_overallWednesday = new ObservableCollection<TkbItem>()); }
            private set { _overallWednesday = value; }
        }

        public ObservableCollection<TkbItem> OverallThursday
        {
            get { return _overallThursday ?? (_overallThursday = new ObservableCollection<TkbItem>()); }
            private set { _overallThursday = value; }
        }

        public ObservableCollection<TkbItem> OverallFriday
        {
            get { return _overallFriday ?? (_overallFriday = new ObservableCollection<TkbItem>()); }
            private set { _overallFriday = value; }
        }

        public ObservableCollection<TkbItem> OverallSaturday
        {
            get { return _overallSaturday ?? (_overallSaturday = new ObservableCollection<TkbItem>()); }
            private set { _overallSaturday = value; }
        }

        private void ClearOverallProperty()
        {
            OverallMonday.Clear();
            OverallTuesday.Clear();
            OverallWednesday.Clear();
            OverallThursday.Clear();
            OverallFriday.Clear();
            OverallSaturday.Clear();
            OverallSunday.Clear();
        }
    }
}
