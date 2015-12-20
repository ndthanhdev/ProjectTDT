using ProjectTDTWindows.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProjectTDTWindows.Services;
using System.Windows.Input;
using ProjectTDTWindows.Common;

namespace ProjectTDTWindows.ViewModels
{
    public class MainViewModel
    {
        private TKBViewModel _TKB;   

        public string Name
        {
            get
            {
                return SettingServices.NameOfUser;
            }
        }

        public TKBViewModel TKB {
            set
            {
                _TKB = value;
            }
            get
            {
                if (_TKB == null) _TKB = new TKBViewModel();
                return _TKB;
            }
        }
        
        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand( () => 
                {
                     Logout();
                });
            }
        }

        public MainViewModel()
        {
        }

        public async Task LoadData()
        {
            
            await TKB.LoadData();
            await TKB.UpdateData();          
            
        }

        public void Logout()
        {
            SettingServices.IsLogged = false;
          ///  await ProjectTDTShared.Services.TKBDataServices.Delete();
            SettingServices.NameOfUser = "";
            Services.NavigationServices.NavigateToPage("LoginPage");            
        }
    }
}
