using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Respond;

namespace TDTUniversal.DataContext
{
    public class ThongBao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EntryId { get; set; }
        public string Title { get; set; }
        public bool IsNew { get; set; }

        public ThongBao() { }
        public ThongBao(ThongBaoRaw raw)
        {
            EntryId = raw.Id;
            IsNew = raw.IsNew;
            Title = raw.Title;
        }
    }
}
