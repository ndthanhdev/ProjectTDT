using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using XTDT.UWP.Common;

namespace XTDT.UWP.Converters
{
    public class TietToBeginTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tiet = value.ToString();
            var result = Ultility.GetBeginTime(tiet);
            return result.ToString(@"h\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
