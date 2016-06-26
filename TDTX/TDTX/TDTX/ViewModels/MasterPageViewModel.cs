using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TDTX.ViewModels.Base;
using TDTX.Views;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public class MasterPageViewModel:BindableObject
    {
        private static MasterPageViewModel _instance;
        public static MasterPageViewModel Instance => _instance ?? new MasterPageViewModel();
        public ObservableCollection<MasterPageItem> Items { get; set; }
        public string Title { get; }
        private MasterPageViewModel()
        {
            Title = "Menu";
            Items = new ObservableCollection<MasterPageItem>()
            {
                new MasterPageItem() {Title = "Login", TargetType = typeof(LoginPage)},
                new MasterPageItem() {Title = "Test", TargetType = typeof(TestPage)}
            };
            _instance = this;
        }

        public void AddItem()
        {
            Items.Add(new MasterPageItem() {Title = "new",TargetType = typeof(TestPage)});
            OnPropertyChanged(nameof(Title));
        }
    }
}
