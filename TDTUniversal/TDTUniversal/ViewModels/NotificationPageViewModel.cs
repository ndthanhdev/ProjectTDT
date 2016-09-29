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
        private int _threshold = 1;
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

        private bool _isUpdating = false;
        public bool IsUpdating
        {
            get { return _isUpdating; }
            set
            {
                _isUpdating = value;
                RaisePropertyChanged(nameof(IsRunning));
            }
        }

        private int _numOfQuests;

        public int NumOfQuests
        {
            get { return _numOfQuests; }
            set
            {
                if (value >= 0)
                {
                    _numOfQuests = value;
                    RaisePropertyChanged(nameof(IsRunning));
                }
            }
        }

        public bool IsRunning
        {
            get { return IsUpdating || NumOfQuests > 0; }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set { Set(ref _isOpen, value); }
        }

        private Uri _viewSource;

        public Uri ViewSource
        {
            get { return _viewSource ?? (_viewSource = new Uri("http://api.trautre.cf")); }
            set { Set(ref _viewSource, value); }
        }



        public ICommand LoadMoreCommand => new DelegateCommand(async () =>
         {
             await GetNotifications();
         }, () => !IsUpdating);

        public ICommand MakeAsReadCommand => new DelegateCommand<ThongBao>(async (tb) => await ReadThongBao(tb, false));
        public ICommand ReadCommand => new DelegateCommand<ThongBao>(async (tb) => await ReadThongBao(tb, true));

        public ICommand HidePopup => new DelegateCommand(() => IsOpen = false);

        public async Task LoadData()
        {
            await Task.Yield();
            try
            {
                NumOfQuests++;
                using (TDTContext db = new TDTContext())
                {
                    ThongBaos = new ObservableCollection<ThongBao>((from tb in db.ThongBao select tb).OrderByDescending(tb => tb.EntryId));
                }
            }
            catch { }
            finally { NumOfQuests--; }
        }

        public async Task UpdateData()
        {
            await Task.Yield();
            await GetNotifications();
        }

        public async Task GetNotifications()
        {
            try
            {
                IsUpdating = true;
                await Task.Yield();
                _currentPage++;
                if (_currentPage > _threshold)
                    return;
                var package = await ApiClient.GetAsync<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest(LocalDataService.Instance.StudentID, _currentPage),
                    TokenService.GetTokenProvider());
                if (!package.Status)
                {
                    _currentPage--;
                    return;
                }
                _threshold = package.Respond.Numpage;
                using (TDTContext db = new TDTContext())
                {
                    if (_currentPage == 1)
                    {
                        db.ThongBao.RemoveRange(from tb in db.ThongBao select tb);
                        ThongBaos.Clear();
                    }
                    var newtblist = from tbr in package.Respond.Thongbao select new ThongBao(tbr);
                    foreach (ThongBao tb in newtblist)
                    {
                        ThongBaos.Add(tb);
                    }
                    db.AddRange(newtblist);
                    await db.SaveChangesAsync();
                }
            }
            catch { }
            finally
            {
                IsUpdating = false;
            }

        }

        public async Task<string> GetNotificationsContent(ThongBao tb)
        {
            try
            {
                NumOfQuests++;
                await Task.Yield();
                _currentPage++;
                var package = await ApiClient.GetAsync<ThongBaoContentRequest, string>(new ThongBaoContentRequest(LocalDataService.Instance.StudentID, tb.EntryId),
                    TokenService.GetTokenProvider());
                if (!package.Status)
                    return string.Empty;
                tb.IsNew = false;
                RaisePropertyChanged(nameof(ThongBaos));
                return package.Respond;
            }
            catch { }
            finally
            {
                NumOfQuests--;
            }
            return string.Empty;
        }

        public async Task ReadThongBao(ThongBao thongBao, bool isRead)
        {
            try
            {
                if (IsOpen = isRead)
                {
                    ViewSource = new Uri(await RequestBuilder.BuildUrl
                        (new ThongBaoContentRequest(LocalDataService.Instance.StudentID, thongBao.EntryId), TokenService.GetTokenProvider()));
                }
                else
                {
                    try
                    {
                        NumOfQuests++;
                        await Task.Yield();
                        await ApiClient.GetAsync<ThongBaoContentRequest, string>(new ThongBaoContentRequest(LocalDataService.Instance.StudentID, thongBao.EntryId), TokenService.GetTokenProvider());
                    }
                    catch { }
                    finally
                    {
                        NumOfQuests--;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
