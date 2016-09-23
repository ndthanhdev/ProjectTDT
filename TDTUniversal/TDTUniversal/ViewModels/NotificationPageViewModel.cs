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
    public class NotificationPageViewModel : ViewModelBase
    {
        private const int _numberOfRequest = 3;
        private const int _maximunPageSave = 10;

        ObservableCollection<ThongBao> _thongBaos;
        public ObservableCollection<ThongBao> ThongBaos
        {
            get { return _thongBaos ?? (_thongBaos = new ObservableCollection<ThongBao>()); }
            set { Set(ref _thongBaos, value); }
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
        }

        public async Task UpdateData()
        {
            await Task.Yield();
            var first = await ApiClient.GetAsync<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest(LocalDataService.Instance.StudentID),
                TokenService.GetTokenProvider());
            if (!first.Status)
                return;
            using (TDTContext db = new TDTContext())
            {
                db.ThongBao.RemoveRange(from tb in db.ThongBao select tb);
                db.ThongBao.AddRange(from tbr in first.Respond.Thongbao select new ThongBao(tbr));
                ThongBaos = new ObservableCollection<ThongBao>(from tb in db.ThongBao select tb);
            }
            //List<Task<Package<DSThongBaoRequest, DSThongBao>>> quests = new List<Task<Package<DSThongBaoRequest, DSThongBao>>>();
            //var range = Math.Min(_maximunPageSave, first.Respond.Numpage);
            //for (int i = 2; i <= range; i += _numberOfRequest)
            //{
            //    for (int j = i; j <= Math.Min(i + _numberOfRequest, range); j++)
            //    {
            //        var t = ApiClient.GetAsync<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest(LocalDataService.Instance.StudentID, i),
            //        TokenService.GetTokenProvider());
            //        quests.Add(t);
            //    }
            //    await Task.WhenAll(quests);
            //}
            var newData = new List<ThongBao>();
        }
    }
}
