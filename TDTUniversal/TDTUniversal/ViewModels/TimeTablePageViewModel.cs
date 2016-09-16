using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API;
using TDTUniversal.API.Requests;
using TDTUniversal.API.Respond;
using TDTUniversal.DataContext;
using TDTUniversal.Services;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace TDTUniversal.ViewModels
{
    public class TimeTablePageViewModel : ViewModelBase
    {
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

        private ObservableCollection<HocKy> _hocKyList;
        public ObservableCollection<HocKy> HocKyList
        {
            get { return _hocKyList ?? (_hocKyList = new ObservableCollection<HocKy>() { new HocKy() { TenHocKy = "123" } }); }
            set { Set(ref _hocKyList, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await base.OnNavigatedToAsync(parameter, mode, state);
            await LoadData();
            await UpdateData();
        }
        public async Task LoadData()
        {
            await Task.Yield();
            using (TDTContext context = new TDTContext())
            {
                var ls = context.HocKy.ToList();
                HocKyList.Clear();
                foreach (var hk in ls)
                    HocKyList.Add(hk);
            }
        }
        public async Task UpdateData()
        {
            try
            {
                await Task.Yield();
                Quest++;
                using (TDTContext context = new TDTContext())
                {
                    var dstthk = await ApiClient.GetAsync<DSHocKyRequest, List<ThongTinHocKy>>(new DSHocKyRequest(LocalDataService.Instance.StudentID),
                                 TokenService.GetTokenProvider());
                    var dshk = from tthk in dstthk.Respond select new HocKy() { HocKyId = tthk.Id, TenHocKy = tthk.TenHocKy };
                    foreach (var hk in dshk)
                    {
                        if (context.HocKy.Contains(hk))
                            context.HocKy.Update(hk);
                        else
                            context.HocKy.Add(hk);
                    }
                    await context.SaveChangesAsync();
                    var ls = context.HocKy.ToList();
                    ls.Reverse();
                    HocKyList = new ObservableCollection<HocKy>(ls);
                    List<Task<Package<HocKyDataRequest,HocKyData>>> works = new List<Task<Package<HocKyDataRequest, HocKyData>>>();
                    for(int i = 0;i<3;i++)
                    {
                        works.Add(ApiClient.GetAsync<HocKyDataRequest, HocKyData>(
                            new HocKyDataRequest(HocKyList[i].HocKyId,LocalDataService.Instance.StudentID), 
                            TokenService.GetTokenProvider()));
                    }
                    await Task.WhenAll(works);
                    await context.SaveChangesAsync();

                    var mh = context.MonHoc.ToList();
                    var lich = context.LichHoc.ToList();
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                Quest--;
            }
        }

        //private async Task UpdateHocKyData(HocKy hk, TDTContext db)
        //{
        //    await Task.Yield();
        //    var data = await ApiClient.GetAsync<HocKyDataRequest, HocKyData>(new HocKyDataRequest(hk.HocKyId), TokenService.GetTokenProvider());
        //    hk.NgayBatDau = data.Respond.Start;
        //    db.Update(hk);
        //    if (data.Respond.TKB == null)
        //        return;
        //    foreach (var mh in data.Respond.TKB)
        //    {
        //        var entityMH = new MonHoc() { HocKy = hk.HocKyId, MaMH = mh.MaMH, TenMH = mh.TenMH, Nhom = mh.Nhom, To = mh.To };
        //        if (db.MonHoc.Contains(entityMH))
        //            db.MonHoc.Update(entityMH);
        //        else
        //            db.MonHoc.Update(entityMH);

        //        var remove = from lh in db.LichHoc where lh.HocKy == hk.HocKyId && lh.TenMH == entityMH.TenMH select lh;
        //        db.LichHoc.RemoveRange(remove);

        //        foreach (var lich in mh.Lich)
        //        {
        //            var entityLich = new DataContext.LichHoc()
        //            {
        //                HocKy = hk.HocKyId,
        //                TenMH = mh.TenMH,
        //                Phong = lich.Phong,
        //                Tiet = lich.Tiet,
        //                Thu = lich.Thu,
        //                Tuan = lich.Tuan
        //            };
        //            db.Add(entityLich);
        //        }
        //    }
        //}

    }

}

