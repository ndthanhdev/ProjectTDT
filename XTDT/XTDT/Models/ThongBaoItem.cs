using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using XTDT.API.Respond;

namespace XTDT.Models
{
    public class ThongBaoItem
    {
        private static string _checkNewRegex = @"\((0[1-9]|[12][0-9]|3[01]])\-(0[1-9]|1[012])-\d{4}\) Mới ";
        private static string _dateRegex = @"(0[1-9]|[12][0-9]|3[01]])\-(0[1-9]|1[012])-\d{4}";
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Publish { get; set; }
        public bool IsNew { get; set; }
        public ThongBaoItem() { }
        public ThongBaoItem(ThongBao tb)
        {
            Id = tb.Id;

            Regex rg = new Regex(_checkNewRegex);
            IsNew = rg.IsMatch(tb.Title);


            rg = new Regex(_dateRegex, RegexOptions.RightToLeft);
            var match = rg.Match(tb.Title);
            Publish = DateTime.ParseExact(match.Value, "dd-mm-yyyy", CultureInfo.InvariantCulture);

            Title = tb.Title.Substring(0, match.Index - 1);
        }
    }
}
