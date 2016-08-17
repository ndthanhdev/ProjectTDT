using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class ThongTinHocKy
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("TenHocKy")]
        public string TenHocKy { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is ThongTinHocKy)
            {
                var other = (obj as ThongTinHocKy);
                return other.Id == Id && other.TenHocKy == TenHocKy;
            }
            return base.Equals(obj);
        }
    }
}
