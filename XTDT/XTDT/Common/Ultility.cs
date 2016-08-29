using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTDT.Common
{
    public static class Ultility
    {
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
