using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TDTX.Views.TimeTableSubs
{
	public partial class DayPage : ContentPage
	{
		public DayPage ()
		{
			InitializeComponent ();                    
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            //Label = new Label() { FormattedText}
        }
    }
}
