using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using TDTX.Models;
using TDTX.Views;
using TDTX.Views.Base;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private static MasterPageViewModel _instance;
        public static MasterPageViewModel Instance => _instance ?? new MasterPageViewModel();

        private ObservableCollection<MasterPageItem> _items;

        public ObservableCollection<MasterPageItem> Items
        {
            get { return _items = _items ?? new ObservableCollection<MasterPageItem>(); }
            set { Set(ref _items, value); }
        }
        private MasterPageViewModel()
        {
            _items = new ObservableCollection<MasterPageItem>()
            {
                //new MasterPageItem() {Title = "Option", TargetType = typeof(OptionPage)},
                new MasterPageItem() {Title = "Time table",TargetType = typeof(TimeTablePage),IconSource = "Images/calendar.png"},
                new MasterPageItem() {Title = "Notification",TargetType = typeof(NotificationPage),IconSource = "Images/worldwide.png"},
                new MasterPageItem() {Title = "Tuition",TargetType = typeof(TuitionPage),IconSource = "Images/piggy-bank.png"},

                //new MasterPageItem() {Title = "Settings", TargetType = typeof(TestPage), IconSource = "Images/settings.png"},
                //new MasterPageItem() {Title = "Settings", TargetType = typeof(TestPage), IconSource = "Images/settings.png"},
                //new MasterPageItem() {Title = "Settings", TargetType = typeof(TestPage), IconSource = "Images/settings.png"},

                //new MasterPageItem() {Title = "Settings", TargetType = typeof(TestPage)},
            };
            _instance = this;
        }

        public void AddItem()
        {
            Items.Add(new MasterPageItem() { Title = "new", TargetType = typeof(TestPage) });
        }
    }
}
