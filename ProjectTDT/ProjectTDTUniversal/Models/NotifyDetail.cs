using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Models
{
    public class NotifyDetail
    {
        private Dictionary<string, Uri> _attach;
    

        public Dictionary<string, Uri> Attach
        {
            get { return _attach ?? new Dictionary<string, Uri>();          }
            private set
            {
                _attach = value ?? new Dictionary<string, Uri>();
            }
        }

        public string Content
        {
            get;
            private set;            
        }


        public NotifyDetail(string content, Dictionary<string, Uri> attach)
        {
            Attach = attach;
            Content = content;            
        }
    }
}
