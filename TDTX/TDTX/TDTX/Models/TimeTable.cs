using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TDTX.API;
using TDTX.Base;
using TDTX.Common;
using TDTX.Services;
using TDTX.ViewModels;
using Xamarin.Forms;

namespace TDTX.Models
{
    public class TimeTable : ILocalObject
    {
        private static TimeTable _instance;
        public static TimeTable Instance => _instance ?? new TimeTable();

        private TimeTable()
        {
            _instance = this;
        }

        private DictionarySerializeToArray<SemesterInfor, Semester> _semesterDictionary;
        /// <summary>
        /// provide infor of all semester ready to use
        /// </summary>
        public DictionarySerializeToArray<SemesterInfor, Semester> SemesterDictionary
        {
            get { return _semesterDictionary = _semesterDictionary ?? new DictionarySerializeToArray<SemesterInfor, Semester>(); }
            set
            {
                _semesterDictionary = value;
            }
        }

        public string Key => "TimeTable.txt";
    }
}
