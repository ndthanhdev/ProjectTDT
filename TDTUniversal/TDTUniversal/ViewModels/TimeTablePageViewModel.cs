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
    public partial class TimeTablePageViewModel : ViewModelBase
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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await base.OnNavigatedToAsync(parameter, mode, state);
            await LoadData();
            await UpdateData();
        }
        public async Task LoadData()
        {
            try
            {
                await Task.Yield();
                Quest++;
                await Task.WhenAll(Task.Run(() =>
                {
                    using (TDTContext db = new TDTContext())
                    {
                        var ls = db.HocKy.ToArray();
                        HocKyList = new ObservableCollection<HocKy>(ls);
                    }
                }), UpdateAgenda());
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                Quest--;
            }

        }
        public async Task UpdateData()
        {
            try
            {
                await Task.Yield();
                Quest++;
                using (TDTContext db = new TDTContext())
                {
                    await Task.Yield();
                    await UpdateHocKy();
                    HocKyList = new ObservableCollection<HocKy>(db.HocKy.ToArray());
                    //List<Task> quest = new List<Task>();
                    for (int i = 0; i < Math.Min(4, HocKyList.Count); i++)
                    {
                        await UpdateMonHocLichHoc(HocKyList[i]);
                    }
                    //await Task.WhenAll(quest);
                    await UpdateAgenda();
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally { Quest--; }
        }

        public async Task UpdateHocKy()
        {
            try
            {
                await Task.Yield();
                Quest++;
                using (TDTContext db = new TDTContext())
                {

                    var dstthk = await ApiClient.GetAsync<DSHocKyRequest, List<ThongTinHocKy>>(new DSHocKyRequest(LocalDataService.Instance.StudentID),
                 TokenService.GetTokenProvider());
                    if (dstthk.Respond == null)
                        return;
                    var dshk = from tthk in dstthk.Respond select new HocKy() { HocKyId = tthk.Id, TenHocKy = tthk.TenHocKy };
                    //lấy danh sách học kỳ không còn tồn tại
                    var removesHK = from hk in db.HocKy where !dshk.Contains(hk) select hk;
                    db.HocKy.RemoveRange(removesHK);
                    //lấy danh sách id hk ko còn tồn tại
                    var removesHKID = from hk in removesHK select hk.HocKyId;
                    //xóa các mh và lh dư
                    db.MonHoc.RemoveRange(from mh in db.MonHoc where removesHKID.Contains(mh.HocKyId) select mh);
                    db.LichHoc.RemoveRange(from lh in db.LichHoc where removesHKID.Contains(lh.HocKyId) select lh);
                    foreach (var hk in dshk)
                    {
                        if (db.HocKy.Contains(hk))
                        {
                            //todo update TenHK
                        }
                        else
                            db.HocKy.Add(hk);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                Quest--;
            }

        }

        private async Task UpdateMonHocLichHoc(HocKy hk)
        {
            try
            {
                await Task.Yield();
                Quest++;
                using (TDTContext db = new TDTContext())
                {
                    var data = await ApiClient.GetAsync<HocKyDataRequest, HocKyData>(new HocKyDataRequest(hk.HocKyId, LocalDataService.Instance.StudentID), TokenService.GetTokenProvider());
                    if (data.Respond == null || data.Respond.TKB == null)
                        return;
                    hk.NgayBatDau = data.Respond.Start;
                    db.Update(hk);
                    db.MonHoc.RemoveRange(from mh in db.MonHoc where mh.HocKyId == hk.HocKyId select mh);
                    db.LichHoc.RemoveRange(from lh in db.LichHoc where lh.HocKyId == hk.HocKyId select lh);

                    /// cũ
                    //var listEntityMH = from mh in data.Respond.TKB select new MonHoc() { HocKy = hk.HocKyId, MaMH = mh.MaMH, TenMH = mh.TenMH, Nhom = mh.Nhom, To = mh.To };
                    ////lấy danh sách môn học không tồn tại trong hk hiện tại
                    //var listRemoveMh = from mh in db.MonHoc where mh.HocKy == hk.HocKyId && !listEntityMH.Contains(mh) select mh;
                    ////xóa mh ko còn tồn tại
                    //db.MonHoc.RemoveRange(listRemoveMh);

                    ////lấy tên mh không còn tồn tại
                    ////var listRemoveTenMH = from mh in listRemoveMh select mh.TenMH;
                    ////lấy lh không còn tồn tại
                    //var removeLichHoc = from mh in listRemoveMh select new DataContext.LichHoc() { HocKy = hk.HocKyId, TenMH = mh.TenMH };
                    ////var removeLichHoc = from lh in db.LichHoc where lh.HocKy == hk.HocKyId && listRemoveTenMH.Contains(lh.TenMH) select lh;
                    ////xóa lh không còn tồn tại
                    //db.LichHoc.RemoveRange(removeLichHoc);

                    foreach (var mh in data.Respond.TKB)
                    {
                        var entityMH = new MonHoc() { HocKyId = hk.HocKyId, MaMH = mh.MaMH, TenMH = mh.TenMH, Nhom = mh.Nhom, To = mh.To };
                        //cũ
                        ////clear old 
                        //if (db.MonHoc.Contains(entityMH))
                        //    db.MonHoc.Update(entityMH);
                        //else

                        db.MonHoc.Add(entityMH);

                        //var remove = from lh in db.LichHoc where lh.HocKy == hk.HocKyId && lh.TenMH == entityMH.TenMH select lh;
                        //db.LichHoc.RemoveRange(remove);

                        foreach (var lich in mh.Lich)
                        {
                            var entityLich = new DataContext.LichHoc()
                            {
                                HocKyId = hk.HocKyId,
                                TenMH = mh.TenMH,
                                Phong = lich.Phong,
                                Tiet = lich.Tiet,
                                Thu = lich.Thu,
                                Tuan = lich.Tuan
                            };
                            db.Add(entityLich);
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Quest--;
            }
        }

    }

}

