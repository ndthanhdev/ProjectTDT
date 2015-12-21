using ProjectTDTUniversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectTDTUniversal.Common
{
    public static class Serializer
    {
        public static IEnumerable<Notify> DeserializerNotify(string NotifyWebSource)
        {
            List<Notify> result = new List<Notify>();

            List<string> strings = new List<string>(StringHelper.RegexStrings(NotifyWebSource, TemplatesRegexPatterns.GetNotify));
            XmlSerializer serializer = new XmlSerializer(typeof(NotifyRaw));

            var raws = from item in strings select
                     (NotifyRaw)serializer.Deserialize(XmlReader.Create(new System.IO.StringReader(item)));

            var notifys = from item in raws select new Notify()
            {
                Title = item.span[0].Value,
                Link=new Uri("https://student.tdt.edu.vn"+item.href),
                // Date= DateTime.Parse(item.span[1].)
                
            };
            


            return result;
        }
    }
}
