using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.Models;
using TDTX.ViewModels;
using Xamarin.Forms;

namespace TDTX.Views.TimeTableSubs
{
    public partial class OverallPage : ContentPage
    {
        public OverallPage()
        {
            InitializeComponent();
            SemesterListChanged(TimeTablePageViewModel.Instance.SemesterInforList);
            //use when semester list updated
            MessagingCenter.Subscribe<TimeTablePageViewModel, IList<SemesterInfor>>(this, "SemesterListChanged",
                (sender, list) => SemesterListChanged(list));
        }

        private async void SemesterListChanged(IList<SemesterInfor> newList)
        {
            await Task.Yield();
            SemesterPicker.Items.Clear();
            foreach (SemesterInfor si in newList)
                SemesterPicker.Items.Add(si.TenHocKy);
        }

    }
}
