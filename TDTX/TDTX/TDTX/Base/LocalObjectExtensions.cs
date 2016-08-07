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
            await Task.Yield();
            if (Application.Current.Properties.ContainsKey(current.Key))
            {
                string text = (string)Application.Current.Properties[current.Key];
                current = JsonConvert.DeserializeObject<T>(text);
            }
        }

        public static async Task Save(this ILocalObject current)
        {
            await Task.Yield();
            string text = JsonConvert.SerializeObject(current, Formatting.Indented);
            Application.Current.Properties[current.Key] = text;
        }

        public static async Task Delete(this ILocalObject current)
        {
            await Task.Yield();
            Application.Current.Properties[current.Key] = string.Empty;
        }
    }

}