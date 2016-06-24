using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Views;

namespace TDTX.ViewModels
{
    public class MasterPageViewModel
    {
        private static MasterPageViewModel _instance;
        public static MasterPageViewModel Instance => _instance ?? new MasterPageViewModel();

        public List<MasterPageItem> items { get; set; }
        private MasterPageViewModel()
        {
            items= new List<MasterPageItem>();
            items.Add(new MasterPageItem() {Title = "Login", TargetType = typeof(LoginPage)});
            _instance = this;
        }
    }
}
