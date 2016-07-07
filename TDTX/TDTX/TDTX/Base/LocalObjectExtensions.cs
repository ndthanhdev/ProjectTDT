using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Dependencies;
using Xamarin.Forms;

namespace TDTX.Base
{
    public static class LocalObjectExtensions
    {
        public static async Task Load<T>(this LocalObject current) where T:LocalObject
        {
            if (await DependencyService.Get<ISaveAndLoad>().FileExists(current.FileName))
            {
                string text = await DependencyService.Get<ISaveAndLoad>().LoadTextAsync(current.FileName);
                current = JsonConvert.DeserializeObject<T>(text);
            }
        }
    }
}
