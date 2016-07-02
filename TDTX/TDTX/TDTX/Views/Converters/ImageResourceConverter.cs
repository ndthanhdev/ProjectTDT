using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TDTX.Views.Base;
using Xamarin.Forms;

namespace TDTX.Views.Converters
{
    public class ImageResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageProvider.GetImageResource(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
