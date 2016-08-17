using System;
using System.Collections.Generic;
using System.Text;
using XTDT.API.Respond;

namespace XTDT.Models
{
    public class TkbItem : IComparable<TkbItem>
    {
        public Tkb Tkb { get; set; }
        public Lich Lich { get; set; }

        public static int Compare(TkbItem x, TkbItem y)
        {
            int length = Math.Min(x.Lich.Tiet.Length, y.Lich.Tiet.Length);
            for (int i = 0; i < length; i++)
            {
                if (x.Lich.Tiet[i] != '-' || y.Lich.Tiet[i] != '-')
                {
                    if (x.Lich.Tiet[i] == '-')
                        return 1;
                    if (y.Lich.Tiet[i] == '-')
                        return -1;
                    for (int j = i; j < length; j++)
                    {
                        if (x.Lich.Tiet[i] == '-' || y.Lich.Tiet[i] == '-')
                        {
                            if (x.Lich.Tiet[i] != '-')
                                return 1;
                            if (y.Lich.Tiet[i] != '-')
                                return -1;
                        }
                    }
                    return 0;
                }
            }
            return 0;
        }
        public int CompareTo(TkbItem other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Compare(this, other);
        }
    }
}
