using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TDTX.Views
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();          
		}
	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        Application.Current.Resources["Bcolor"] = Color.Aqua;
	    }
	}
}
