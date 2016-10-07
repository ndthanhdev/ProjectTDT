using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDTUniversal.API.Respond;

namespace TDTUniversal.DataContext
{
    public class ThongBao : Template10.Mvvm.BindableBase
    {
        public static readonly Regex REGEX_MATCH_DATE = new Regex(@"(?<=\()((((0[1-9]|1[0-9]|2[0-8])-(0[1-9]|1[012]))|((29|30|31)-(0[13578]|1[02]))|((29|30)-(0[4,6,9]|11)))-(19|[2-9][0-9])\d\d)|(29-02-(19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96))(?=\))", RegexOptions.RightToLeft);

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EntryId { get; set; }
        public string Title { get; set; }

        private bool _isNew;

        public bool IsNew
        {
            get { return _isNew; }
            set { Set(ref _isNew, value); }
        }

        public DateTime PublishDate { get; set; }


        public ThongBao() { }
        public ThongBao(ThongBaoRaw raw)
        {
            EntryId = raw.Id;
            IsNew = raw.IsNew;
            Title = raw.Title;
            var dateString = REGEX_MATCH_DATE.Match(raw.Title).Value;
            PublishDate = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

    }
}
