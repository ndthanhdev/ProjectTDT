using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Dependencies;
using Xamarin.Forms;

namespace TDTX.Base
{
    [JsonObject]
    public abstract class LocalObject
    {
        [JsonIgnore]
        public abstract string FileName { get; }
        public virtual async Task Save()
        {
            string text = JsonConvert.SerializeObject(this);
            await DependencyService.Get<ISaveAndLoad>().SaveTextAsync(FileName, text);
        }

    }
}
