using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;

namespace XTDT.UWP.Models
{
    public class PageItem
    {
        public string Glyph { get; set; }
        public Type PageType { get; set; }
        public string Name { get; set; }
    }
}
