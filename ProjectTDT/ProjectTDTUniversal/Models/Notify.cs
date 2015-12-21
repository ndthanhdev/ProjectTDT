using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Models
{
    public class Notify
    {
        public string Title { get; set; }

        public Uri Link { get; set; }

        public bool IsNew { get; set; }

        public string Date { get; set; }
    }
}
