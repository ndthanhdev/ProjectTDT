using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private int _currentPage = 0;
        private const int _threshold = 10;
        private bool _isRunning = false;

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

        public ICommand LoadMoreCommand => new DelegateCommand(async () =>
         {
             await GetNoti();
         }, () => !_isRunning);

        public async Task LoadData()
        {
            await Task.Yield();
        }

        public async Task UpdateData()
        {
            await Task.Yield();
            await GetNoti();
        }

        public async Task GetNoti()
        {
            _isRunning = true;
            await Task.Yield();
            _currentPage++;
            var package = await ApiClient.GetAsync<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest(LocalDataService.Instance.StudentID, _currentPage),
                TokenService.GetTokenProvider());
            if (!package.Status)
                return;
            using (TDTContext db = new TDTContext())
            {
                if (_currentPage == 1)
                {
                    db.ThongBao.RemoveRange(from tb in db.ThongBao select tb);
                }
                var newtblist = from tbr in package.Respond.Thongbao select new ThongBao(tbr);
                foreach (ThongBao tb in newtblist)
                {
                    ThongBaos.Add(tb);
                }
                db.AddRange(newtblist);
                await db.SaveChangesAsync();
            }
            _isRunning = false;

        }
    }
}
