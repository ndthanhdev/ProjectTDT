using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ProjectTDTWindows.Converters
{
    public class BoolToBrush: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                
                return  new SolidColorBrush( Colors.WhiteSmoke);
            }
            else
                return new SolidColorBrush(Colors.Silver);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
