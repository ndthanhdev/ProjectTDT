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
            await TimeTable.Instance.Load<TimeTable>();

            if (Settings.Instance.CanTryLogin())
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
            await Settings.Instance.Save();
            await TimeTable.Instance.Save();
        }

        protected override async void OnResume()
        {
            // Handle when your app resumes
            //await Settings.Instance.Load<Settings>();
            //await TimeTable.Instance.Load<TimeTable>();

        }
    }
}
