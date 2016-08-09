using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TDTX.Common
{
    public static class ObservableCollectionExtensions
    {
        public static void AddToOrdered<T>(this ObservableCollection<T> source, T item, bool increase = true) where T : IComparable<T>
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
