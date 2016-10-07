using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.DataContext;
using TDTUniversal.ViewModels;
using Windows.UI.Xaml.Data;

namespace TDTUniversal.Converters
{
    public class ThongBaoToGrouped : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return new ObservableCollection<GroupedThongBao>();
            var thongBaos = (value as ObservableCollection<ThongBao>);
            ObservableCollection<GroupedThongBao> groups = new ObservableCollection<GroupedThongBao>();
            var query = from item in thongBaos
                        group item by item.PublishDate into g
                        orderby g.Key
                        select new { GroupName = g.Key, Items = g };
            foreach (var g in query)
            {
                GroupedThongBao info = new GroupedThongBao();
                info.Key = g.GroupName;
                foreach (var item in g.Items)
                {
                    info.Add(item);
                }
                groups.Add(info);
            }
            return groups;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
}
