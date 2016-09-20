using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace TDTUniversal.Converters
{
    public class TietToBeginTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tiet = value.ToString();
            var result = GetBeginTime(tiet);
            return result.ToString(@"h\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        public static TimeSpan GetBeginTime(string tiet)
        {
            for (int i = 0; i < tiet.Length; i++)
            {
                if (tiet[i] != '-')
                {
                    switch (i)
                    {
                        case 0:
                            return TimeSpan.Parse("6:50");
                        case 1:
                            return TimeSpan.Parse("7:35");
                        case 2:
                            return TimeSpan.Parse("8:30");
                        case 3:
                            return TimeSpan.Parse("9:25");
                        case 4:
                            return TimeSpan.Parse("10:10");
                        case 5:
                            return TimeSpan.Parse("11:05");
                        case 6:
                            return TimeSpan.Parse("12:30");
                        case 7:
                            return TimeSpan.Parse("13:15");
                        case 8:
                            return TimeSpan.Parse("14:10");
                        case 9:
                            return TimeSpan.Parse("15:05");
                        case 10:
                            return TimeSpan.Parse("15:50");
                        case 11:
                            return TimeSpan.Parse("16:45");
                        case 12:
                            return TimeSpan.Parse("17:45");
                        case 13:
                            return TimeSpan.Parse("18:30");
                        case 14:
                            return TimeSpan.Parse("19:30");
                        case 15:
                            return TimeSpan.Parse("20:15");
                    }
                }
            }
            return TimeSpan.FromSeconds(0);
        }

    }
}
