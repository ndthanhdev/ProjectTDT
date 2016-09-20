using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.DataContext;
using Template10.Mvvm;

namespace TDTUniversal.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        private ObservableCollection<HocKy> _hocKyList;
        public ObservableCollection<HocKy> HocKyList
        {
            get { return _hocKyList ?? (_hocKyList = new ObservableCollection<HocKy>()); }
            set
            {
                var old = SelectedHK;
                if (value.Contains(old))
                {
                    Set(ref _hocKyList, new ObservableCollection<HocKy>(value.OrderByDescending(h => h.HocKyId)));
                    SelectedHK = old;
                }
                else
                {
                    Set(ref _hocKyList, new ObservableCollection<HocKy>(value.OrderByDescending(h => h.HocKyId)));
                }
            }
        }

        private ObservableCollection<DataContext.LichHoc> _overallSunday;
        private ObservableCollection<DataContext.LichHoc> _overallMonday;
        private ObservableCollection<DataContext.LichHoc> _overallTuesday;
        private ObservableCollection<DataContext.LichHoc> _overallWednesday;
        private ObservableCollection<DataContext.LichHoc> _overallThursday;
        private ObservableCollection<DataContext.LichHoc> _overallFriday;
        private ObservableCollection<DataContext.LichHoc> _overallSaturday;

        public ObservableCollection<DataContext.LichHoc> OverallSunday
        {
            get { return _overallSunday ?? (_overallSunday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallSunday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallMonday
        {
            get { return _overallMonday ?? (_overallMonday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallMonday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallTuesday
        {
            get { return _overallTuesday ?? (_overallTuesday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallTuesday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallWednesday
        {
            get { return _overallWednesday ?? (_overallWednesday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallWednesday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallThursday
        {
            get { return _overallThursday ?? (_overallThursday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallThursday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallFriday
        {
            get { return _overallFriday ?? (_overallFriday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallFriday, value); }
        }
        public ObservableCollection<DataContext.LichHoc> OverallSaturday
        {
            get { return _overallSaturday ?? (_overallSaturday = new ObservableCollection<DataContext.LichHoc>()); }
            private set { Set(ref _overallSaturday, value); }
        }

        private HocKy _selectedHK;
        public HocKy SelectedHK
        {
            get { return _selectedHK; }
            set { Set(ref _selectedHK, value); }
        }

        public System.Windows.Input.ICommand SelectHKCommand => new DelegateCommand(async () => await SelectHK());
        public async Task SelectHK()
        {
            await UpdateUITongQuat();
            if (SelectedHK != null)
            {
                await UpdateMonHocLichHoc(SelectedHK);
                await UpdateUITongQuat();
            }
        }

        public Task UpdateUITongQuat()
        {
            Task.Yield();
            using (TDTContext db = new TDTContext())
            {


                if (SelectedHK == null)
                    return Task.CompletedTask;
                var listMH = from mh in db.MonHoc where mh.HocKyId == SelectedHK.HocKyId select mh;
                var listLH = new List<DataContext.LichHoc>();
                foreach (var mh in listMH)
                {
                    listLH.AddRange(from lh in db.LichHoc where lh.TenMH == mh.TenMH && lh.HocKyId == mh.HocKyId select lh);
                }
                listLH.Sort();
                OverallSunday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 1 select lh);
                OverallMonday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 2 select lh);
                OverallTuesday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 3 select lh);
                OverallWednesday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 4 select lh);
                OverallThursday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 5 select lh);
                OverallFriday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 6 select lh);
                OverallSaturday = new ObservableCollection<DataContext.LichHoc>(from lh in listLH where lh.Thu == 7 select lh);
                return Task.CompletedTask;
            }
        }


    }
}
