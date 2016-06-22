using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TDTX
{
	public class App : Application
	{
		public App ()
		{
   //         // The root page of your application
   //         MainPage = new ContentPage {
   //             Content = new StackLayout {
   //                 VerticalOptions = LayoutOptions.Center,
   //                 Children = {
   //                     new WebView {
   //                         Source = new UrlWebViewSource
   //                         {
   //                             Url = "http://blog.xamarin.com/"
   //                         },
   //                         VerticalOptions = LayoutOptions.FillAndExpand
   //                     },
   //                     new Label()
   //                     {
   //                         Text="showed"
   //                     }
			//		}
			//	}
			//};
            MainPage = new Views.LoginPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts         
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
