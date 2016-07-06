using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using TDTX.Dependencies;

namespace TDTX
{
    public partial class App : Application
    {
        public async Task InitializeSetting()
        {
            //TODO: load setting before app run
            if( DependencyService.Get<ISaveAndLoad>().FileExists("temp.txt"))
                await DependencyService.Get<ISaveAndLoad>().LoadTextAsync("temp.txt");
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(Settings.Current, Formatting.Indented);
        }
    }


}
