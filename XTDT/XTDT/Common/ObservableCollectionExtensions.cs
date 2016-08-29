using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTDT.Common
{
    public static class CollectionExtensions
    {
        public static void AddToOrdered<T>(this IList<T> source, T item, bool increase = true) where T : IComparable<T>
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (!(item.CompareTo(source[i]) < 0 ^ increase))
                {
                    source.Insert(i, item);
                    return;
                }
            }
            source.Add(item);
        }
    }
}
