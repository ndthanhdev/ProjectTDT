using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TDTX.Models;
using Xamarin.Forms;

namespace TDTX.Views.Converters
{
    public class TimeTableItemToShortString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeTableItem)
            {
                TimeTableItem tti = value as TimeTableItem;
                string tiet = tti.Schedule.tiet.Replace("-", string.Empty);
                return string.Join(" ", tiet, tti.Schedule.phong, tti.Course.TenMH);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
