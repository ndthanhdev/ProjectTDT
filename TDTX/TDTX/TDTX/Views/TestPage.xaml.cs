using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TDTX.Views
{
	public partial class TestPage : ContentPage
	{
		public TestPage ()
		{
			InitializeComponent ();
            if (App.Current.Properties.ContainsKey("text"))
            {
                this.EntryText.Text = App.Current.Properties["text"].ToString();
            }

        }

        private void Button_OnClicked(object sender, EventArgs e)
	    {
	        ViewModels.MasterPageViewModel.Instance.AddItem();
	    }

	    private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
	    {
	        App.Current.Properties["text"] = this.EntryText.Text;
	    }
	}
}
