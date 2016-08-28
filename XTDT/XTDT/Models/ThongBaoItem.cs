using System;
using System.Collections.Generic;
using System.Text;
using XTDT.API.Respond;

namespace XTDT.Models
{
    public class ThongBaoItem
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Publish { get; set; }
        public bool IsNew { get; set; }
        public ThongBaoItem(){}
        public ThongBaoItem(ThongBao tb)
        {
            Title = tb.Title;
            Id = tb.Title;

        }
    }
}
