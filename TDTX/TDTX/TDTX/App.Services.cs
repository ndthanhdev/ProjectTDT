using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Base;
using Xamarin.Forms;
using TDTX.Dependencies;

namespace TDTX
{
    public partial class App : Application
    {
        public async Task InitializeSetting()
        {
            await Task.Yield();
        }
    }


}
