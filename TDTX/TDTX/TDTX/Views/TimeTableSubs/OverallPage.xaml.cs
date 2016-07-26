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
            SemesterListChanged(TimeTablePageViewModel.Instance.SemesterDictionary);
            //use when semester list updated
            MessagingCenter.Subscribe<TimeTablePageViewModel, IDictionary<SemesterInfor, Semester>>(this, "SemesterDictionaryChanged",
                (sender, dic) => SemesterListChanged(dic));
        }

        private async void SemesterListChanged(IDictionary<SemesterInfor, Semester> newDic)
        {
            await Task.Yield();
            SemesterPicker.Items.Clear();
            foreach (var si in newDic.Keys)
                SemesterPicker.Items.Add(si.TenHocKy);
        }

        private void SemesterPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TimeTablePageViewModel.Instance.UpdateOverall();
        }
    }
}
