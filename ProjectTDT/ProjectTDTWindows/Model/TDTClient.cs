using ProjectTDTWindows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTDTWindows.Model
{
    public class TDTClient
    {
        public string MSSV { get; set; }
        public string MK { get; set; }
        public bool isLogged { get; private set; }
        public string Name { get; private set; }

        private HttpClient Client;

        public TDTClient(string MSSV, string MK)
        {

            this.MSSV = MSSV;
            this.MK = MK;


            Client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = true });
            Client.BaseAddress = new Uri("http://dkmh.tdt.edu.vn");
            this.isLogged = false;
            Client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            Client.DefaultRequestHeaders.AcceptCharset.ParseAdd("utf-8");
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 Firefox/40.0");
        }

        public async Task<bool> tryLogin()
        {
            try
            {
                if (!Common.InternetConnection.IsInternetAvailable())
                {
                    return false;
                }
                FormUrlEncodedContent formdata = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("SinhVienLogin1$txtMSSV", MSSV),
                    new KeyValuePair<string, string>("SinhVienLogin1$txtTruongHoc",MK),
                    new KeyValuePair<string, string>("__EVENTTARGET","SinhVienLogin1$cmdDangNhap")
                });
                HttpResponseMessage Response = await Client.PostAsync("/login.aspx", formdata);                
                string webSource = await Response.Content.ReadAsStringAsync();
                isLogged = (webSource.IndexOf("SinhVienLogin1_cmdDangNhap") < 0 && !string.IsNullOrEmpty(webSource)) ? true : false;
                if (isLogged)
                {
                    Regex reg = new Regex("<span id=\"SinhVienLogin1_lblHoTen\".+</span>");
                    Name = XElement.Parse(reg.Match(webSource).Value).Value;
                }
                return isLogged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// <para>Login but don't get student's Name</para>
        /// <para>This method replace for tryLogin method on WP which can't auto redirect.</para>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> tryLoginAnonymous()
        {
            try
            {
                if (!Common.InternetConnection.IsInternetAvailable())
                {
                    return false;
                }
                FormUrlEncodedContent formdata = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("SinhVienLogin1$txtMSSV", MSSV),
                    new KeyValuePair<string, string>("SinhVienLogin1$txtTruongHoc",MK),
                    new KeyValuePair<string, string>("__EVENTTARGET","SinhVienLogin1$cmdDangNhap")
                });
                HttpResponseMessage Response = await Client.PostAsync("/login.aspx", formdata);
                string webSource = await Response.Content.ReadAsStringAsync();
                isLogged = (webSource.IndexOf("SinhVienLogin1_cmdDangNhap") < 0 && !string.IsNullOrEmpty(webSource)) ? true : false;
                return isLogged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendTKBMessage(TKBMessage TKBMessage, string HKID = "0")
        {
            try
            {
                if (!Common.InternetConnection.IsInternetAvailable())
                {
                    throw new Exception("No internet connection");
                }
                HttpResponseMessage Respone = new HttpResponseMessage();
                if (isLogged == false)
                {
                    throw new Exception("not logged yet");
                }
                if (TKBMessage.isFirstRequest)
                {
                    Respone = await Client.GetAsync("/tkb2.aspx");
                    TKBMessage.Update(Respone);
                }
                if (TKBMessage.SelectedHocKy != HKID && HKID != "0")
                {
                    FormUrlEncodedContent formdatahk = new FormUrlEncodedContent(new[]
                    {
                      new KeyValuePair<string, string>("ThoiKhoaBieu1$cboHocKy",HKID),
                      new KeyValuePair<string,string>("__EVENTTARGET","ThoiKhoaBieu1$cboHocky"),
                      new KeyValuePair<string, string>("__VIEWSTATE",TKBMessage.CurrentVIEWSTATE)
                    });
                    Respone = await Client.PostAsync(TKBMessage.RequestUri, formdatahk);
                    TKBMessage.Update(Respone);

                }
                if (HKID != "0")
                {
                    FormUrlEncodedContent formdatatonghop = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("ThoiKhoaBieu1$radChonLua","radXemTKBTongHop"),
                         new KeyValuePair<string,string>("__EVENTTARGET","ThoiKhoaBieu1$radXemTKBTongHop"),
                         new KeyValuePair<string, string>("__VIEWSTATE",TKBMessage.CurrentVIEWSTATE)
                     });
                    Respone = await Client.PostAsync(Respone.RequestMessage.RequestUri, formdatatonghop);
                    TKBMessage.Update(Respone);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: SendTKBMessage", ex);
            }
        }

        public async Task<IEnumerable<Semester>> GetTKBModels(int startIndex = 79)
        {
            try
            {
                var SIMDic = await TKBDataServices.GetSemesterDictionary();
                List<Semester> result = new List<Semester>();
                TKBMessage Message = new TKBMessage();
                await SendTKBMessage(Message);                
                var v = Message.ListHocKy;
                if (v != null)
                {
                    int maxID = 0;
                    foreach (var item in v)
                        maxID = Convert.ToInt32(item.Key) > maxID ? Convert.ToInt32(item.Key) : maxID;
                    for (int i = startIndex; i <= maxID; i++)
                    {
                        await SendTKBMessage(Message, i.ToString());
                        if (SIMDic.ContainsKey(i.ToString()))
                            result.Add(new Semester(Message, SIMDic[i.ToString()]));
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("GetTKBModels", ex);
            }
        }

      

    }

    public class TKBMessage
    {
        #region static method
        private static string GetSelectedHocKyfromSource(string WebSource)
        {
            Regex regHocKy = new Regex("<option selected=\"selected\".+</option>");
            return XElement.Parse(regHocKy.Match(WebSource).Value).Attribute("value").Value;
        }
        private static string GetVIEWSTATEfromSource(string WebSource)
        {
            Regex regHocKy = new Regex("<input.+id=\"__VIEWSTATE\".+>");
            return XElement.Parse(regHocKy.Match(WebSource).Value).Attribute("value").Value;
        }
        private static Dictionary<string, string> GetListHocKyfromSource(string WebSource)
        {
            Regex regHocKy = new Regex("<select .+ id=\"ThoiKhoaBieu1_cboHocKy\" (.|\\n)+</select>");
            return (from item in XElement.Parse(regHocKy.Match(WebSource).Value).Elements("option")
                    select new KeyValuePair<string, string>(item.Attribute("value").Value, item.Value)).ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion


        public string RequestUri { set; get; }
        public string CurrentVIEWSTATE { set; get; }
        public string CurrentSource { set; get; }
        public string SelectedHocKy { set; get; }
        public Dictionary<string, string> ListHocKy { set; get; }
        public bool isFirstRequest
        {
            get
            {
                if (RequestUri == null || CurrentVIEWSTATE == null || CurrentSource == null) return true;
                else return false;
            }
        }
        public async void Update(HttpResponseMessage Response)
        {
            CurrentSource = await Response.Content.ReadAsStringAsync();
            CurrentSource= CurrentSource.Replace("&", "-");        
            RequestUri = Response.RequestMessage.RequestUri.ToString();
            CurrentVIEWSTATE = GetVIEWSTATEfromSource(CurrentSource);
            SelectedHocKy = GetSelectedHocKyfromSource(CurrentSource);
            ListHocKy = GetListHocKyfromSource(CurrentSource);
        }
    }

}
