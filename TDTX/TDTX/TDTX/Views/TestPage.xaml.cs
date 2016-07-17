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
        double x, y;
        public TestPage ()
		{
			InitializeComponent ();
            if (App.Current.Properties.ContainsKey("text"))
            {
                this.EntryText.Text = App.Current.Properties["text"].ToString();
            }
        }

     //   private void Button_OnClicked(object sender, EventArgs e)
	    //{
	    //    ViewModels.MasterPageViewModel.Instance.AddItem();
	    //}

	    private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
	    {
	        App.Current.Properties["text"] = this.EntryText.Text;
	    }

	    private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
	    {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    x = Math.Max(0, e.TotalX);
                    break;
                case GestureStatus.Completed:
                    Image i =sender as Image;
                    // Store the translation applied during the pan
                    i.TranslationX= x;
                    break;
            }
        }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PopAsync();
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	    }
	}
}
