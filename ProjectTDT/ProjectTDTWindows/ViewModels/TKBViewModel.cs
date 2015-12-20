using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ProjectTDTWindows.Model;
using ProjectTDTWindows.Services;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ProjectTDTWindows.Common;

namespace ProjectTDTWindows.ViewModels
{
    public class TKBViewModel:INotifyPropertyChanged
    {
        public TKBViewModel()
        {
            IsRunning = false;
        }
        private bool _IsRunning;
        public bool IsRunning
        {
            set
            {
                _IsRunning = value;
                NotifyPropertyChanged("IsRunning");
            }
            get
            {
                return _IsRunning;
            }
        }

        private DateTime _SelectedDay;
        public DateTime SelectedDay
        {
            set
            {
                _SelectedDay = value;
                NotifyPropertyChanged("SelectedDay");
                NotifyPropertyChanged("Lessons");              
               
            }
            get
            {
                if (_SelectedDay == DateTime.MinValue) _SelectedDay = DateTime.Today;
                return _SelectedDay;
            }
        }

        private List<Semester> _Semesters;

        private List<Semester> Semesters
        {
            get
            {
                return _Semesters ?? (_Semesters = new List<Semester>());
            }
            set
            {
                _Semesters = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }            
        }

        public ICommand AddDayCommand
        {
            get
            {
                return new RelayCommandEx<string>((i) =>
                {
                    AddDay(Convert.ToInt32(i));                    
                });

            }
        }

        private void AddDay(int i)
        {
            SelectedDay= SelectedDay.AddDays((double)i);
            
        }

        public IEnumerable<KeyValuePair<bool,Lesson>> Lessons
        {          
            get
            {
                List<KeyValuePair<bool, Lesson>> result = new List<KeyValuePair<bool, Lesson>>();
                if(Semesters!=null)
                foreach(Semester Sem in Semesters)
                {                       
                        foreach(Lesson lesItem in Sem.GetSessionByDOW(SelectedDay.DayOfWeek).Lessons)
                        {
                            if (lesItem.WorkDay.Max() >= SelectedDay) result.Add(new KeyValuePair<bool, Lesson>(lesItem.WorkDay.Exists(d=>d==SelectedDay),lesItem));
                        }                 
                }

                return 
                    result;
            }
        }
        public async Task LoadData()
        {
            IsRunning = true;
            IEnumerable<Semester> temp = await TKBDataServices.Get();
            if( temp.Count()>0)
            {
                Semesters.Clear(); 
                Semesters.AddRange(temp);
                NotifyPropertyChanged("Lessons");
            }
            IsRunning = false;
            
        }
        public async Task UpdateData()
        {
            IsRunning = true;
            try
            {
                var v = CredentialServices.GetCredentialFromLocker();
                TDTClient Client = new TDTClient(v.UserName, v.Password);
                await Client.tryLogin();
                if (Client.isLogged == true)
                {
                    Semesters = new List<Semester>(await Client.GetTKBModels());
                }
                NotifyPropertyChanged("Lessons"); NotifyPropertyChanged("SelectedDay");
                await TKBDataServices.Save(Semesters);
                await LoadData();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            IsRunning = false;
        }


        private List<DateTime> GetListDate(Lesson lesson,SemesterInforModel SIM,int index)
        {
            List<DateTime> _listdate = new List<DateTime>();
            DateTime beginDay = SIM.BeginCourse.AddDays(SIM.BeginWeek * 7).AddDays(index);
            int Count = 0;
            int lastweek = 0;
            foreach (char ch in lesson.Tuan)
            {
                if (ch == '-')
                {
                    Count++;
                }
                else
                {
                    Count++;
                    beginDay= beginDay.AddDays((Count / 10) * 70);
                    if (_listdate.Count > 0)
                        if (lastweek > Convert.ToInt32(ch.ToString())) beginDay= beginDay.AddDays(70);
                    lastweek = Convert.ToInt32(ch.ToString());
                    _listdate.Add(beginDay.AddDays((lastweek-1) * 7));               
                    Count = 0;
                }
            }
            return _listdate;

        }

    }
}
