using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ProjectTDTWindows.Model;

namespace ProjectTDTWindows.ViewModels
{
    public class NewsViewModel
    {
        private ObservableCollection<RssSchema> _Items;

        public ObservableCollection<RssSchema> Items
        {
            get
            {
                return _Items ?? (_Items = new ObservableCollection<RssSchema>());
            }
        }

        public bool IsBusy;
        public NewsViewModel()
        {
            IsBusy = false;
        }
    }
}
