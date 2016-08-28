﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.UWP.Models;
using XTDT.UWP.Services.LocalDataServices;
using XTDT.UWP.Views;

namespace XTDT.UWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private List<PageItem> _pages;
        public List<PageItem> Pages
        {
            get { return _pages ?? (_pages = new List<PageItem>()); }
            set { _pages = value; }
        }

        public string Avatar => LocalDataService.Instance.Avatar;
        public string Name => LocalDataService.Instance.Name;
        public string MSSV => LocalDataService.Instance.StudentID;

        public HomePageViewModel()
        {
            _pages = new List<PageItem>()
            {
                new PageItem() {PageType=typeof(TimeTablePage), Name="Time Table", Glyph="M14,24.999998L17,24.999998C17.552,24.999998 18,25.447998 18,25.999998 18,26.551998 17.552,26.999998 17,26.999998L14,26.999998C13.448,26.999998 13,26.551998 13,25.999998 13,25.447998 13.448,24.999998 14,24.999998z M6,24.999998L9,24.999998C9.552,24.999998 10,25.447998 10,25.999998 10,26.551998 9.552,26.999998 9,26.999998L6,26.999998C5.448,26.999998 5,26.551998 5,25.999998 5,25.447998 5.448,24.999998 6,24.999998z M22,19.999998L25,19.999998C25.552,19.999998 26,20.447998 26,20.999998 26,21.551998 25.552,21.999998 25,21.999998L22,21.999998C21.448,21.999998 21,21.551998 21,20.999998 21,20.447998 21.448,19.999998 22,19.999998z M14,19.999998L17,19.999998C17.552,19.999998 18,20.447998 18,20.999998 18,21.551998 17.552,21.999998 17,21.999998L14,21.999998C13.448,21.999998 13,21.551998 13,20.999998 13,20.447998 13.448,19.999998 14,19.999998z M6,19.999998L9,19.999998C9.552,19.999998 10,20.447998 10,20.999998 10,21.551998 9.552,21.999998 9,21.999998L6,21.999998C5.448,21.999998 5,21.551998 5,20.999998 5,20.447998 5.448,19.999998 6,19.999998z M22,14.999998L25,14.999998C25.552,14.999998 26,15.447998 26,15.999998 26,16.551998 25.552,16.999998 25,16.999998L22,16.999998C21.448,16.999998 21,16.551998 21,15.999998 21,15.447998 21.448,14.999998 22,14.999998z M14,14.999998L17,14.999998C17.552,14.999998 18,15.447998 18,15.999998 18,16.551998 17.552,16.999998 17,16.999998L14,16.999998C13.448,16.999998 13,16.551998 13,15.999998 13,15.447998 13.448,14.999998 14,14.999998z M6,14.999998L9,14.999998C9.552,14.999998 10,15.447998 10,15.999998 10,16.551998 9.552,16.999998 9,16.999998L6,16.999998C5.448,16.999998 5,16.551998 5,15.999998 5,15.447998 5.448,14.999998 6,14.999998z M1.9999999,11.999998L1.9999999,29.999998 29,29.999998 29,11.999998z M1.9999999,4.9999981L1.9999999,9.9999985 29,9.9999985 29,4.9999981 24.956999,4.9999981 24.956999,6.9309998C24.956999,7.483 24.508999,7.931 23.956999,7.9309998 23.404999,7.931 22.956999,7.483 22.956999,6.9309998L22.956999,4.9999981 7.9569993,4.9999981 7.9569993,6.9309998C7.9569993,7.483 7.5089993,7.931 6.9569993,7.9309998 6.4049993,7.931 5.9569993,7.483 5.9569993,6.9309998L5.9569993,4.9999981z M6.9569993,0C7.5089993,0,7.9569993,0.44799995,7.9569993,1L7.9569993,2.9999981 22.956999,2.9999981 22.956999,1C22.956999,0.44799995 23.404999,0 23.956999,0 24.508999,0 24.956999,0.44799995 24.956999,1L24.956999,2.9999981 31,2.9999981 31,31.999998 0,31.999998 0,2.9999981 5.9569993,2.9999981 5.9569993,1C5.9569993,0.44799995,6.4049993,0,6.9569993,0z"},
                new PageItem() {PageType=typeof(HomePage), Name="Notification", Glyph="M19.054906,19.741768L14.048039,20.529358 14.063186,20.659084C14.249083,21.983285 15.299836,23.00002 16.56496,23.00002 17.960989,23.00002 19.095971,21.762046 19.095971,20.23904 19.095971,20.07928 19.083878,19.922199 19.059796,19.768372z M1.8289995,16.606021L4.4699984,22.571033 2.6409988,23.380034 0,17.414022z M21.205005,4.4070072L5.1810083,16.694026 6.4660082,19.69803 26.353004,16.569026z M22.039005,1.2470016L29.193004,18.147028 21.030803,19.430956 21.034698,19.451968C21.075444,19.709723 21.095975,19.972416 21.095975,20.23904 21.095975,22.864033 19.062982,25.000015 16.56496,25.000015 14.301156,25.000015 12.419249,23.245757 12.086288,20.963068L12.071392,20.840287 5.2390084,21.915033 2.7310085,16.051025z M24.856001,0L32.001,16.897998 30.159001,17.676998 23.014001,0.78000069z" }
            };
        }
        public ICommand NavigateCommand => new DelegateCommand<PageItem>(async (pi) =>
        {
            try
            {
                Busy.SetBusy(true);
                await NavigationService.NavigateAsync(pi.PageType);
            }
            catch { }
            finally
            {
                Busy.SetBusy(false);
            }
        });
        public ICommand LogoutCommand => new DelegateCommand(async () =>
        {
            try
            {
                Busy.SetBusy(true);
                LocalDataService.Instance.IsLogged = false;
                await NavigationService.NavigateAsync(typeof(LoginPage));
            }
            catch { }
            finally
            {
                Busy.SetBusy(false);
            }
        });

    }
}
