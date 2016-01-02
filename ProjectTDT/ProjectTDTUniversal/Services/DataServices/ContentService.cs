using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ProjectTDTUniversal.Models;
using ProjectTDTUniversal.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ProjectTDTUniversal.Services.DataServices
{
    public class ContentService
    {
        

        public static  void Refresh(ILiveContent obj)
        {
            try
            {
                Type tp = null;
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (var pro in obj.Properties)
                {
                    dic.Add(pro.Key, obj.GetType().GetProperty(pro.Key).GetValue(obj));
                    tp = obj.GetType().GetProperty(pro.Key).PropertyType.GetGenericArguments().Single();
                    // GetGenericTypeDefinition().ToString();//.GetElementType();
                }
                string data = JsonConvert.SerializeObject(dic, Formatting.Indented);
                Dictionary<string, object> des = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                
                

                var v = ((JArray)des["Notifys"]).Select(i => i.ToObject(tp));

                                
               //noti.Add(new Notify() { IsNew = true, Date = "", Link = new Uri("https://google.com"), Title = "bolo" });
               // foreach (var pro in obj.Properties)
               //     obj.GetType().GetProperty(pro).SetValue(obj, des[pro]);
            }
            catch(Exception ex)
            {

            }

        }
        private  async Task Save(ILiveContent obj)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            
        }

        public static Type ListOfWhat(Object list)
        {
            return ListOfWhat2((dynamic)list);
        }

        private static Type ListOfWhat2<T>(ICollection<T> list)
        {
            return typeof(T);
        }
    }
}
