using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.ViewModels;
using Xamarin.Forms;

namespace TDTX.Views
{
	public partial class TimtableView : TabbedPage
    {
		public TimtableView ()
		{
			InitializeComponent ();            
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await TimeTablePageViewModel.Instance.UpdateTask();
        }
    }
}
