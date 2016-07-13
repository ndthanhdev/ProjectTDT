using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Base;
using TDTX.Models;
using TDTX.Services;
using TDTX.Services.API;
using TDTX.Views;
using Xamarin.Forms;

namespace TDTX
{
    public partial class App : Application
    {
        public App()
        {
            Avatar a = new Avatar();
            InitializeComponent();
            MainPage = new MainPage();

        }
        protected override async void OnStart()
        {

            // Handle when your app starts  
            await Settings.Instance.Load<Settings>();
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
            await Settings.Instance.Save();
        }

        //protected override void OnResume()
        //{
        //    // Handle when your app resumes
        //}
    }
}
