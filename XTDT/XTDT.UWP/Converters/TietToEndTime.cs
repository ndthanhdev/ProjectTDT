using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using XTDT.Common;


namespace XTDT.UWP.Converters
{
    public class TietToEndTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tiet = value.ToString();
            var result = Ultility.GetEndTime(tiet);
            return result.ToString(@"h\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
