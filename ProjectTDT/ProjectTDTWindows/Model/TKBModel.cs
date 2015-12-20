using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using ProjectTDTWindows.Model;

namespace ProjectTDTWindows.Model
{
    public class Course
    {
        private DateTime _Now;
        public DateTime Now
        {
            set
            {
                _Now = value;
            }
            get
            {
                if (_Now == DateTime.MinValue) _Now = DateTime.Today;
                return _Now;
            }
        }

        private DateTime _startTime = new DateTime(2015, 8, 10);
        public DateTime StartTime
        {
            get { return _startTime; }
        }
        public DateTime EndTime
        {
            get
            {
                return _startTime.AddDays(363);
            }
        }
        public int CurrentWeek
        {
            get
            {
                TimeSpan time = Now.Subtract(_startTime);
                if (time.Days >= 0) return time.Days / 7;
                else return 0;
            }
        }

        private ObservableCollection<Semester> _semesters;
        public ObservableCollection<Semester> Semesters
        {
            get
            {
                if (_semesters == null) _semesters = new ObservableCollection<Semester>();
                return _semesters;
            }
        }
        public void AddSemester(Semester Semester)
        {
            this.Semesters.Add(Semester);
        }
        public Course(int YearStart, int MonthStart, int DayStart, ObservableCollection<Semester> Semesters)
        {
            _startTime = new DateTime(YearStart, MonthStart, DayStart);
            _semesters = new ObservableCollection<Semester>(Semesters);
        }

        private  ObservableCollection<KeyValuePair<bool, Lesson>> _LessonsNow;
        public ObservableCollection<KeyValuePair<bool, Lesson>> LessonsNow
        {
            get
            {
                if (_LessonsNow == null) _LessonsNow = new ObservableCollection<KeyValuePair<bool, Lesson>>();
                foreach (Semester S in Semesters)
                {
                    Session Ses = S.GetSessionByDOW(Now.DayOfWeek);
                    foreach (Lesson L in Ses.Lessons)
                    {
                        if (CurrentWeek < S.GetWeeks(L).Max())
                        {
                            if (S.GetWeeks(L).IndexOf(CurrentWeek) >= 0)
                                _LessonsNow.Add(new KeyValuePair<bool, Lesson>(true, L));
                            else
                                _LessonsNow.Add(new KeyValuePair<bool, Lesson>(false, L));
                        }

                    }
                }
                return _LessonsNow;
            }
        }

    }
    public class Semester
    {
        private string id = "0";
        private SemesterInforModel SIM { set; get; }
        public string ID { set { id = value; } get { return id; } }

        public Semester() { }

        public Semester(TKBMessage Message, SemesterInforModel SemesterInfor)
        {
            //get id of tkb
            ID = Message.SelectedHocKy;
            //complete SIM
            SIM = SemesterInfor;
            //get rawtkbmodel
            Regex regextbody = new Regex("<table (.|\\n)+/table>");
            string source = regextbody.Match(Message.CurrentSource).Value;
            StringReader reader = new StringReader(source);            
            XmlSerializer xmlserializer = new XmlSerializer(typeof(table));
            table rawmodel = (table)xmlserializer.Deserialize(reader);
            //convert rawmodel to model
            Regex reg = new Regex("-|\\d");
            for (int c = 0; c < 7; c++)
            {
                List<Lesson> temp = new List<Lesson>();
                for (int r = 0; r < 3; r++)
                {
                    temp.AddRange(from item in rawmodel.tr[r + 1].td[c + 1].span
                                  select new Lesson()
                                  {
                                      MonHoc = item.p.b,
                                      MaMH_Nhom = item.p.textField[0],
                                      Tiet_Phong = item.p.textField[1],
                                      Tuan = string.Join("", reg.Matches(item.p.textField[2]).Cast<Match>().Select(v => v.Value)),
                                      WorkDay = GetWorkDays(string.Join("", reg.Matches(item.p.textField[2]).Cast<Match>().Select(v => v.Value)), c)
                                  });
                }
                Sessions.Add(new Session(temp));
            }
        }
        

        private ObservableCollection<Session> _Sessions;
        public ObservableCollection<Session> Sessions
        {
            set { _Sessions = value; }
            get
            {
                if (_Sessions == null) _Sessions = new ObservableCollection<Session>();
                return _Sessions;
            }
        }
        public List<int> GetWeeks (Lesson lesson)
        {
            List<int> _listweek = new List<int>();
            ResourceLoader loader =new Windows.ApplicationModel.Resources.ResourceLoader();
            var v= loader.GetString(ID);         
            int startWeek = Convert.ToInt32(v);
            int Count = 0;
            foreach (char ch in lesson.Tuan)
            {
                if (ch == '-')
                {
                    Count++;
                }
                else
                {
                    Count++;
                    startWeek += (Count / 10) * 10;
                    if (_listweek.Count > 0)
                        if ((_listweek.Last() % 10) > Convert.ToInt32(ch.ToString())) startWeek += 10;
                    _listweek.Add(startWeek + Convert.ToInt32(ch.ToString()));
                    Count = 0;
                }
            }
            return _listweek;

        }
        public Session GetSessionByDOW(DayOfWeek DOW)
        {
            if (DOW == DayOfWeek.Monday)
                return Sessions[0];
            if (DOW == DayOfWeek.Tuesday)
                return Sessions[1];
            if (DOW == DayOfWeek.Wednesday)
                return Sessions[2];
            if (DOW == DayOfWeek.Thursday)
                return Sessions[3];
            if (DOW == DayOfWeek.Friday)
                return Sessions[4];
            if (DOW == DayOfWeek.Saturday)
                return Sessions[5];
            else
                return Sessions[6];

        }
        private List<DateTime> GetWorkDays(string Tuan, int index)
        {
            List<DateTime> _listdate = new List<DateTime>();
            DateTime beginDay = SIM.BeginCourse.AddDays(SIM.BeginWeek * 7).AddDays(index);
            int Count = 0;
            int lastweek = 0;
            foreach (char ch in Tuan)
            {
                if (ch == '-')
                {
                    Count++;
                }
                else
                {
                    Count++;
                    beginDay = beginDay.AddDays((Count / 10) * 70);
                    if (_listdate.Count > 0)
                        if (lastweek > Convert.ToInt32(ch.ToString())) beginDay = beginDay.AddDays(70);
                    lastweek = Convert.ToInt32(ch.ToString());
                    _listdate.Add(beginDay.AddDays((lastweek - 1) * 7));
                    Count = 0;
                }
            }
            return _listdate;

        }


    }
    public class Session
    {
        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            set { _lessons = value; }
            get
            {
                if (_lessons == null) _lessons = new ObservableCollection<Lesson>();
                return _lessons;
            }
        }

        public Session() { }
        public Session(int numberoflesson)
        {
            for (int i = 0; i < numberoflesson; i++)
                Lessons.Add(new Lesson());
        }
        public Session(IEnumerable<Lesson> Lessons)
        {
            this.Lessons = new ObservableCollection<Lesson>(Lessons);
        }

    }
    public class Lesson
    {
        public string MonHoc { set; get; }
        public string MaMH_Nhom { set; get; }
        public string Tiet_Phong { set; get; }
        public string Tuan { set; get; }
        public List<DateTime> WorkDay { set; get; } 

    }

   
}
