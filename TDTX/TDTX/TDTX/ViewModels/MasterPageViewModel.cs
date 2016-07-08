﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using TDTX.Models;
using TDTX.Views;
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
                //new MasterPageItem() {Title = "Settings", TargetType = typeof(TestPage)},

                new MasterPageItem() {Title = "Login", TargetType = typeof(LoginPage)},
                new MasterPageItem() {Title = "Settings", TargetType = typeof(SettingsPage)},
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
