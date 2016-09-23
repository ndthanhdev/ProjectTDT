using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.DataContext;
using TDTUniversal.Models;
using Template10.Mvvm;

namespace TDTUniversal.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        private ObservableCollection<MonHocLichHoc> _agenda;
        public ObservableCollection<MonHocLichHoc> Agenda
        {
            get { return _agenda ?? (_agenda = new ObservableCollection<MonHocLichHoc>()); }
            set { Set(ref _agenda, value); }
        }


        private DateTimeOffset _selectedDate = DateTimeOffset.Now;
        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                Set(ref _selectedDate, value);
                UpdateAgenda();
            }
        }

        public DelegateCommand<int> ChangeDateCommand => new DelegateCommand<int>((i) =>
        {
            if (i != 0)
                SelectedDate = SelectedDate.AddDays(i);
            else
                SelectedDate = DateTime.Now;
        });

        public Task UpdateAgenda()
        {
            try
            {
                List<MonHocLichHoc> mhlh = new List<MonHocLichHoc>();
                using (TDTContext db = new TDTContext())
                {
                    var listHK = (from hk in db.HocKy where SelectedDate.Date.Subtract(hk.NgayBatDau).Days < 365 select hk).ToArray();
                    foreach (var hk in listHK)
                    {
                        var listMH = from mh in db.MonHoc where mh.HocKyId == hk.HocKyId select mh;
                        foreach (var mh in listMH)
                        {
                            var listLH = (from lh in db.LichHoc
                                          where lh.HocKyId == hk.HocKyId
                                            && lh.TenMH == mh.TenMH
                                            && IsWorkOnDate(lh, hk.NgayBatDau, SelectedDate.Date)
                                          select lh).ToArray();
                            mhlh.AddRange(from lh in listLH select new MonHocLichHoc(mh, lh));
                        }
                    }
                }
                Agenda = new ObservableCollection<MonHocLichHoc>(mhlh.OrderBy(i => i.LichHoc));
            }
            catch (Exception ex)
            { throw ex; }
            return Task.CompletedTask;
        }
        private bool IsWorkOnDate(LichHoc lich, DateTime startDate, DateTime date)
        {
            if (date < startDate)
                return false;
            int differenceDay = date.Subtract(startDate).Days;
            int index = differenceDay / 7;
            if (index > lich.Tuan.Length - 1)
                return false;
            if (lich.Tuan[index] == '-')
                return false;
            return startDate.AddDays(index * 7) + ThuToBonusDay(lich.Thu) == date;
        }
        private TimeSpan ThuToBonusDay(int thu)
        {
            return TimeSpan.FromDays(thu > 1 ? thu - 2 : 6);
        }

    }
}
