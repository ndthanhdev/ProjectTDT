using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TDTX.Views.Converters
{
    public class tietToStartTime:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tiet = value.ToString();
            for (int i = 0; i < tiet.Length; i++)
            {
                if (tiet[i] != '-')
                {
                    switch (i)
                    {
                        case 0:
                            return "6:50";
                        case 1:
                            return "7:35";
                        case 2:
                            return "8:30";
                        case 3:
                            return "9:25";
                        case 4:
                            return "10:10";
                        case 5:
                            return "11:05";
                        case 6:
                            return "12:30";
                        case 7:
                            return "13:15";
                        case 8:
                            return "14:10";
                        case 9:
                            return "15:05";
                        case 10:
                            return "15:50";
                        case 11:
                            return "16:45";
                        case 12:
                            return "17:45";
                        case 13:
                            return "18:30";
                        case 14:
                            return "19:30";
                        case 15:
                            return "20:15";
                    }
                }
            }
            return "unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
