using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Models.Base
{
    public abstract class RESTObject
    {
        [JsonIgnore]
        public abstract string[] Attributes { get; }

        public virtual string GetQuery(params string[] parameters)
        {
            if (Attributes.Length != parameters.Length)
                return null;
            List<string> list = new List<string>();
            for (int i = 0; i < Attributes.Length; i++)
                list.Add(string.Concat(Attributes[i], "=", parameters[i]));
            return "?" + string.Join("&", list);
        }
    }
}
