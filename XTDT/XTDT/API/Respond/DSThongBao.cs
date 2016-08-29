using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class DonVi
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

    public class ThongBao : IComparable<ThongBao>
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is ThongBao)
            {
                return (obj as ThongBao).Id == Id;
            }
            return base.Equals(obj);
        }

        public int CompareTo(ThongBao obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return 1;
            }
            return Id.CompareTo(obj.Id);
        }
    }

    public class DSThongBao
    {
        [JsonProperty("donvi")]
        public IList<DonVi> DonVi { get; set; }

        [JsonProperty("thongbao")]
        public IList<ThongBao> Thongbao { get; set; }

        [JsonProperty("numpage")]
        public int Numpage { get; set; }
    }

}
