using ProjectTDTUniversal.Common;
using ProjectTDTUniversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Security.Credentials;

namespace ProjectTDTUniversal.Services.DataServices
{
    public partial class  Transporter
    {

        /// <summary>
        /// Login and set user's Name
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Login()
        {
            PasswordCredential account = CredentialsService.GetCredential();
            await Transport(TemplatesForm.Login, account.UserName, account.Password);

            string userFullName = StringHelper.RegexString(HttpRepository.Content, TemplatesRegexPatterns.GetUserFullName);
           
            userFullName = StringHelper.MergeLine(userFullName);

            SettingsServices.SettingsService.Instance.UserName = userFullName;
            return !string.IsNullOrEmpty(SettingsServices.SettingsService.Instance.UserName);
        }

        public async Task<IEnumerable<Notify>> GetNotify()
        {
            List<Notify> result = new List<Notify>();

            //get content
            await Transport(TemplatesForm.GetNotify);

            //Scraping raw notify
            List<string> strings = new List<string>(StringHelper.RegexStrings(HttpRepository.Content, TemplatesRegexPatterns.GetNotify));
            XmlSerializer serializer = new XmlSerializer(typeof(NotifyRaw));
            var raws = from item in strings
                       select  (NotifyRaw)serializer.Deserialize(XmlReader.Create(new System.IO.StringReader(item)));

            //remix raw
            result.AddRange(from item in raws  select new Notify()
                            {
                                Title = StringHelper.MergeLine(item.span[0].Value),
                                Link = new Uri("https://student.tdt.edu.vn" + item.href),
                                Date = item.span[1].Value,
                                IsNew = item.span[0].style.IndexOf("bold") > -1
                            });
            return result;
        }

        public async Task GetNotifyContent(Uri link)
        {
            try
            {

                await Transport(new HttpForm(new Uri("https://student.tdt.edu.vn/thongbao/741")));                
                //string textContent =string.Join("\n",
                //    from item in StringHelper.RegexStrings(HttpRepository.Content, TemplatesRegexPatterns.GetPtag) select StringHelper.StripHTML(item));
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(HttpRepository.Content);
                var v= doc.DocumentNode.Descendants("div").Where(node=> node.Attributes.Contains("class") && node.Attributes["class"].Value.Contains("rnews-article-content"));

                string s1 = StringHelper.StripHTML(v.ElementAt(1).InnerHtml);
                


            }
            catch(Exception ex)
            {

            }


        }
             
            

    }
}
