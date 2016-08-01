using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TDTX.Dependencies;
using Xamarin.Forms;
using System.Reflection;
using System.Runtime.Serialization;

namespace TDTX.Base
{
    public static class LocalObjectExtensions
    {
        public static async Task Load<T>(this ILocalObject current) where T : ILocalObject
        {
            if (await DependencyService.Get<ISaveAndLoad>().FileExists(current.FileName))
            {
                string text = await DependencyService.Get<ISaveAndLoad>().LoadTextAsync(current.FileName);
                current = JsonConvert.DeserializeObject<T>(text);
            }
        }

        public static async Task Save(this ILocalObject current)
        {
            string text = JsonConvert.SerializeObject(current, Formatting.Indented);
            await DependencyService.Get<ISaveAndLoad>().SaveTextAsync(current.FileName, text);
        }

        public static async Task Delete(this ILocalObject current)
        {
            await DependencyService.Get<ISaveAndLoad>().SaveTextAsync(current.FileName, "");
        }
    }

}