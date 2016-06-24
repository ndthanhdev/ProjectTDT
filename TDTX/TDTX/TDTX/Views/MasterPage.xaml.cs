using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.ViewModels;
using Xamarin.Forms;

namespace TDTX.Views
{
    public partial class MasterPage : ContentPage
    {
        //public ListView ListView { get { return PrimaryListView; } }
        public MasterPage()
        {
            InitializeComponent();
            //this.BindingContext = MasterPageViewModel.Instance;
            //this.PrimaryListView.ItemsSource = MasterPageViewModel.Instance.items;
        }

    }
}

