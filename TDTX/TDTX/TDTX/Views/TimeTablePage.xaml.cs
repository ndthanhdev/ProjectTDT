using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TDTX.ViewModels;
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
            DetailPage = new SettingsPage();
            Children.Add(DetailPage);
            BackgroundLayout.SizeChanged += BackgroundLayout_SizeChanged;
            TimeTablePageViewModel.Instance.Navigated += page =>
            {
                DetailPage = page;
                DetailPage?.Layout(ContentLayout.Bounds);
            };
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


    }
}
