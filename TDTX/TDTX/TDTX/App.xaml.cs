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
            MainPage = new SplashPage();
        }

        private bool _loaded = false;

        protected override async void OnStart()
        {
            await Task.Yield();

            // Handle when your app starts  
            await Task.WhenAll(Settings.Instance.Load<Settings>(), TimeTable.Instance.Load<TimeTable>());
            _loaded = true;
            if (Settings.Instance.CanTryLogin())
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
        }

        protected override async void OnSleep()
        {
            //await Task.WhenAll(Settings.Instance.Save(), TimeTable.Instance.Save());

            await Task.Yield();
            if (_loaded)
            {
                await Task.WhenAll(Settings.Instance.Save(), TimeTable.Instance.Save());
            }
            await this.SavePropertiesAsync();
        }

        protected override async void OnResume()
        {
            // Handle when your app resumes
            if (_loaded)
            {
                await Task.WhenAll(Settings.Instance.Load<Settings>(), TimeTable.Instance.Load<TimeTable>());
            }
        }
    }
}
