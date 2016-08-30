using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.API.Respond;
using XTDT.Common;
using XTDT.Models;

namespace XTDT.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        private ObservableCollection<TkbItem> _agenda;
        public ObservableCollection<TkbItem> Agenda
        {
            get { return _agenda ?? (_agenda = new ObservableCollection<TkbItem>()); }
            set { Set(ref _agenda, value); }
        }
        private DateTimeOffset _selectedDate = DateTimeOffset.Now;
        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                Set(ref _selectedDate, value);
                UpdateAgenda(_selectedDate.Date);
            }
        }

        public Task UpdateAgenda(DateTime selectedDate)
        {
            List<TkbItem> tempList = new List<TkbItem>();
            foreach (var keyValue in DataCotroller.HocKyDictionary)
            {
                if (keyValue.Value == null || keyValue.Value.Tkb == null || keyValue.Value.Start > selectedDate
                    || selectedDate.Subtract(keyValue.Value.Start).Days > 365)
                    continue;
                foreach (var tkb in keyValue.Value.Tkb)
                {
                    foreach (var lich in tkb.Lich)
                    {
                        if (IsWorkOnDate(lich, keyValue.Value.Start, selectedDate))
                            tempList.Add(new TkbItem()
                            {
                                Tkb = tkb,
                                Lich = lich
                            });
                    }
                }
            }
            Agenda.Clear();
            foreach (var tkbItem in tempList)
                Agenda.AddToOrdered(tkbItem);
            return Task.CompletedTask;
        }

        private bool IsWorkOnDate(Lich lich, DateTime startDate, DateTime date)
        {
            if (date < startDate)
                return false;
            int differenceDay = date.Subtract(startDate).Days;
            int index = differenceDay / 7;
            if (index > lich.Tuan.Length - 1)
                return false;
            if (lich.Tuan[index] == '-')
                return false;
            return startDate.AddDays(index * 7) + ThuToBonusDay(lich.Thu) == date;
        }

        public ICommand ChangeDateCommand => new DelegateCommand<int>((i) =>
        {
            if (i != 0)
                SelectedDate = SelectedDate.AddDays(i);
            else
                SelectedDate = DateTime.Now;
        });
    }
}
