using ProjectTDTWindows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjectTDTWindows.Services
{
    public class RssProvider
    {
        public static async Task<IEnumerable<RssSchema>> GetRSS()
        {
            try
            {
                Regex reg = new Regex(@"(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?");
                string source = await HtmlProvider.GetHtml("http://tdt.edu.vn/index.php/tin-tuc/tin-tuc-su-kien?format=feed&amp;type=rss");
                XDocument xdoc = XDocument.Parse(source);
                List<XElement> lxe = new List<XElement>(xdoc.Descendants("item"));
                List<RssSchema> lrss = new List<RssSchema>();
                foreach (var item in lxe)
                {
                    RssSchema it = new RssSchema();
                    it.Title = item.Element("title").Value;
                    it.ImageUri = reg.Match(item.Element("description").Value).Value;
                    it.Url = item.Element("link").Value;
                    lrss.Add(it);
                }
                return lrss;
            }
            catch(Exception ex)
            {
                return new List<RssSchema>();
                throw new Exception("Can't get Rss",ex);
            }
        }
    }
}
