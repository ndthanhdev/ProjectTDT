using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTDT.IServices;

namespace XTDT.ConsoleApplication1.Services.BusyServies
{
    public class BusyService : IBusyService
    {
        public static IBusyService Instance { get; } = new BusyService();

        private BusyService() { }
        public void SetBusy(bool busy, string text = null)
        {
            
        }
    }
}
