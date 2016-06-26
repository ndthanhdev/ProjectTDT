using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TDTX.Views
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            ((MasterPage) Master).PrimaryListView.ItemSelected += PrimaryListView_ItemSelected;
		}

        private void PrimaryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                ((MasterPage)Master).PrimaryListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
