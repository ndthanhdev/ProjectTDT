using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Models
{
    public class NotifyDetail:Template10.Mvvm.BindableBase
    {

        private ObservableCollection<KeyValuePair<string, Uri>> _attach;

        public ObservableCollection<KeyValuePair<string, Uri>> Attach
        {
            get
            {
                return _attach ?? new ObservableCollection<KeyValuePair<string, Uri>>();
            }
            set
            {
                //_attach = new List<KeyValuePair<string, Uri>>(value);
                Set(ref _attach, value?? new ObservableCollection<KeyValuePair<string, Uri>>());
                RaisePropertyChanged("Attach");
            }
        }       

        public string Content
        {
            get;
            private set;            
        }


        public NotifyDetail(string content, ObservableCollection<KeyValuePair<string, Uri>> attach)
        {
            Attach = attach;
            Content = content;            
            
        }
    }
}
