using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.API.Respond;
using XTDT.Models;
using XTDT.UWP.Common;

namespace XTDT.UWP.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        public Task UpdateOverallKey()
        {
            try
            {
                Quest++;
                //TODO for prevent from reset selected
                Task.Yield();
                var current = SelectedTTHK;
                HocKyList.Clear();
                foreach (var tthk in DataCotroller.HocKyDictionary.Keys)
                    HocKyList.Add(tthk);
                SelectedTTHK = HocKyList.Contains(current) ? current : (HocKyList.Count > 0 ? HocKyList[0] : null);
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
            finally { Quest--; }
        }
        public async Task UpdateOverallValue(ThongTinHocKy tthk)
        {
            try
            {
                Quest++;
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
            catch { }
            finally
            {
                Quest--;
            }

        }
        public ICommand SelectHocKyCommand => new DelegateCommand<ThongTinHocKy>(async (tthk) => await UpdateOverallValue(tthk));

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
