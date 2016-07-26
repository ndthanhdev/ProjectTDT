using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.API;
using TDTX.Base;
using TDTX.Models;
using TDTX.Services;
using TDTX.ViewModels;
using TDTX.Views;
using Xamarin.Forms;

namespace TDTX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage= new SplashPage();
        }
        protected override async void OnStart()
        {
            // Handle when your app starts  
            await Settings.Instance.Load<Settings>();
            if (Settings.Instance.CanTryLogin())
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
            await TimeTablePageViewModel.Instance.Load<TimeTablePageViewModel>();
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
            await Settings.Instance.Save();
            await TimeTablePageViewModel.Instance.Save();
        }

        //protected override void OnResume()
        //{
        //    // Handle when your app resumes
        //}
    }
}
