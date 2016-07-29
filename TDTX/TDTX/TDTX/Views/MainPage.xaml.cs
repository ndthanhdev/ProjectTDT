using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TDTX.Models;
using Xamarin.Forms;

namespace TDTX.Views
{
    public delegate void dv();

    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            ((MasterPage)Master).PrimaryListView.ItemSelected += PrimaryListView_ItemSelected;
            ((MasterPage)Master).SettingTapGesture.Command = PageSelect;
            //((MasterPage) Master).SettingTapGesture.CommandParameter = typeof(SettingsPage);
            this.IsPresentedChanged += MainPage_IsPresentedChanged;

        }

        /// <summary>
        /// Make DetailPage dim while MasterPage is appearring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_IsPresentedChanged(object sender, EventArgs e)
        {
            if (IsPresented)
            {
                this.Detail.Opacity = 0.3;
                this.Detail.BackgroundColor = Color.Black;
            }
            else
            {
                this.Detail.Opacity = 1;
                this.Detail.BackgroundColor = Color.White;
            }
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
        public RelayCommand<Type> PageSelect => new RelayCommand<Type>(t =>
         {
             Detail = new NavigationPage((Page)Activator.CreateInstance(t));
             ((MasterPage)Master).PrimaryListView.SelectedItem = null;

             IsPresented = false;
         });
    }
}
