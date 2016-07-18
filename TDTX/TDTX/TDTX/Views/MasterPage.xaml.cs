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
        public ListView PrimaryListView => PrimaryListViewElement;
        public TapGestureRecognizer SettingTapGesture => this.SettingTapGestureRecognizer;
        public MasterPage()
        {
            InitializeComponent();
        }

    }
}

