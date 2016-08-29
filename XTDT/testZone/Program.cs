using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTDT.API.Respond;
using XTDT.DataController;
using XTDT.Models;

namespace testZone
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                NotificationDataController controller = new NotificationDataController();
                await controller.UpdateKeys("51403318", "51403318TDT");
                await controller.UpdateValues("51403318", "51403318TDT");
                foreach (var dv in controller.TbDictionary.Keys)
                {
                    Console.WriteLine(dv.Title);
                    foreach (var tb in controller.TbDictionary[dv])
                        Console.WriteLine(string.Format("\t{0} [{1}] [{2}]", tb.Title, tb.Publish, tb.IsNew));
                }

            });
            Console.ReadKey();
        }
    }
}
