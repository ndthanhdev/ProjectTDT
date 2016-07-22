using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TDTX.ViewModels;
using TDTX.Views.TimeTableSubs;
using Xamarin.Forms;

namespace TDTX.Views
{
    public partial class TimeTablePage : MultiPage<Page>
    {
        private Page DetailPage
        {
            get
            {
                if (Children.Count < 2)
                    return null;
                return Children[1];
            }
            set
            {
                Children.Add(value);
                while (Children.Count > 2)
                    Children.RemoveAt(1);
            }
        }
        public TimeTablePage()
        {

            InitializeComponent();
            Navigated(TimeTablePageViewModel.Instance.Detail);
            BackgroundLayout.SizeChanged += BackgroundLayout_SizeChanged;
            TimeTablePageViewModel.Instance.Navigated = Navigated;
        }

        private void BackgroundLayout_SizeChanged(object sender, EventArgs e)
        {
            if (ContentLayout.Bounds.Height > 0)
                DetailPage?.Layout(ContentLayout.Bounds);

        }

        protected override Page CreateDefault(object item)
        {
            var p = new ContentPage();
            p.Content = new Label() { Text = "time table default" };
            return p;
        }

        private void Navigated(Page page)
        {
            DetailPage = page;
            DetailPage?.Layout(ContentLayout.Bounds);
            UpdateAppBarUI(DetailPage.GetType());
        }

        private void UpdateAppBarUI(Type t)
        {
            //reset
            DayBtb.Image = "Images/day.png";
            DayBtb.TextColor = Color.Gray;
            WeekBtb.Image = "Images/week.png";
            WeekBtb.TextColor = Color.Gray;
            MonthBtb.Image = "Images/month.png";
            MonthBtb.TextColor = Color.Gray;
            OverallBtb.Image = "Images/overall.png";
            OverallBtb.TextColor = Color.Gray;

            if (t.Equals(typeof(DayPage)))
            {
                DayBtb.Image = "Images/day selected.png";
                DayBtb.TextColor = Color.FromHex("#006DF0");
            }
            else if (t.Equals(typeof(WeekPage)))
            {
                WeekBtb.Image = "Images/week selected.png";
                WeekBtb.TextColor = Color.FromHex("#006DF0");
            }
            else if (t.Equals(typeof(MonthPage)))
            {
                MonthBtb.Image = "Images/month selected.png";
                MonthBtb.TextColor = Color.FromHex("#006DF0");
            }
            else if (t.Equals(typeof(OverallPage)))
            {
                OverallBtb.Image = "Images/overall selected.png";
                OverallBtb.TextColor = Color.FromHex("#006DF0");
            }


        }
    }
}
