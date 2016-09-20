using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;


namespace TDTUniversal.Converters
{
    public class TietToEndTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tiet = value.ToString();
            var result = GetEndTime(tiet);
            return result.ToString(@"h\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public static TimeSpan GetEndTime(string tiet)
        {
            for (int i = tiet.Length - 1; i >= 0; i--)
            {
                if (tiet[i] != '-')
                {
                    switch (i)
                    {
                        case 0:
                            return TimeSpan.Parse("7:35");
                        case 1:
                            return TimeSpan.Parse("8:30");
                        case 2:
                            return TimeSpan.Parse("9:15");
                        case 3:
                            return TimeSpan.Parse("10:10");
                        case 4:
                            return TimeSpan.Parse("11:05");
                        case 5:
                            return TimeSpan.Parse("11:50");
                        case 6:
                            return TimeSpan.Parse("13:15");
                        case 7:
                            return TimeSpan.Parse("14:10");
                        case 8:
                            return TimeSpan.Parse("14:55");
                        case 9:
                            return TimeSpan.Parse("15:50");
                        case 10:
                            return TimeSpan.Parse("16:45");
                        case 11:
                            return TimeSpan.Parse("17:30");
                        case 12:
                            return TimeSpan.Parse("18:30");
                        case 13:
                            return TimeSpan.Parse("19:25");
                        case 14:
                            return TimeSpan.Parse("20:15");
                        case 15:
                            return TimeSpan.Parse("21:00");
                    }
                }
            }
            return TimeSpan.FromSeconds(0);
        }


    }
}
