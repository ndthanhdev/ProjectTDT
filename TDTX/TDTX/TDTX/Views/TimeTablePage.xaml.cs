using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TDTX.Views
{
    public partial class TimeTablePage : MultiPage<Page>
    {
        private SettingsPage sp;
        public TimeTablePage()
        {
            InitializeComponent();
            sp= new SettingsPage();
            Children.Add(sp);
            BackgroundLayout.SizeChanged += BackgroundLayout_SizeChanged;
        }

        private  void BackgroundLayout_SizeChanged(object sender, EventArgs e)
        {
            if (ContentLayout.Bounds.Height > 0)
                sp.Layout(ContentLayout.Bounds);
        }

        protected override Page CreateDefault(object item)
        {
            var p = new ContentPage();
            p.Content = new Label() { Text = "time table default" };
            return p;
        }
    }
}
