using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Models.Base
{
    /// <summary>
    /// add property to subclass
    /// </summary>
    public abstract class RESTObject
    {
        public static string[] BasicAttributes => new[] { "user", "pass", "act" };

        private string[] _attributes;

        [JsonIgnore]
        public virtual string[] Attributes => _attributes ?? BasicAttributes;

        public virtual string GetQuery(params object[] values)
        {
            if (Attributes.Length != values.Length)
                return null;
            List<string> list = new List<string>();
            for (int i = 0; i < Attributes.Length; i++)
                list.Add(string.Concat(Attributes[i], "=", values[i]));
            return "?" + string.Join("&", list);
        }
    }
}
