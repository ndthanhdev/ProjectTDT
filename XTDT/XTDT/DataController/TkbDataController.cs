using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTDT.API.Material;
using XTDT.API.Requests;
using XTDT.API.Respond;
using XTDT.API.ServiceAccess;
using XTDT.Common;

namespace XTDT.DataController
{
    public class TkbDataController
    {
        private static TkbDataController _instance;
        public static TkbDataController Instance => _instance ?? (_instance = new TkbDataController());

        public TkbDataController()
        {
            _instance = this;
        }

        private DictionarySerializeToArray<ThongTinHocKy, HocKy> _hocKyDictionary;
        public DictionarySerializeToArray<ThongTinHocKy, HocKy> HocKyDictionary
        {
            get { return _hocKyDictionary ?? (_hocKyDictionary = new DictionarySerializeToArray<ThongTinHocKy, HocKy>()); }
            set { _hocKyDictionary = value; }
        }

        public async Task<bool> UpdateAsync(string id, string password)
        {
            await Task.Yield();
            if (!await UpdateHocKyDictionaryKey(id, password))
                return false;
            //TODO try multi requesta
            List<Task> provideSemesters = new List<Task>();
            for (int i = 0; i < Math.Min(HocKyDictionary.Count, 6); i++)
                provideSemesters.Add(ProvideHocKyValue(id, password, HocKyDictionary.Keys.ElementAt(i)));
            await Task.WhenAll(provideSemesters);
            return true;
        }

        private async Task<bool> UpdateHocKyDictionaryKey(string id, string password)
        {
            var respond = await Transporter.Transport<HocKyListRequest, List<ThongTinHocKy>>(
                new HocKyListRequest()
                {
                    user = id,
                    pass = password
                });
            if (respond.Status != PackageStatusCode.OK)
                return false;
            var newDic = new DictionarySerializeToArray<ThongTinHocKy, HocKy>();
            foreach (var si in respond.Respond)
            {
                if (HocKyDictionary.ContainsKey(si))
                    newDic[si] = HocKyDictionary[si];
                else
                    newDic[si] = null;
            }
            HocKyDictionary = newDic;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">index of Dictionary element</param>
        /// <returns></returns>
        public async Task<bool> ProvideHocKyValue(string id, string password, ThongTinHocKy key)
        {
            try
            {
                var respond = await Transporter.Transport<HocKyRequest, HocKy>(new HocKyRequest()
                {
                    user = id,
                    pass = password,
                    id = key.Id
                });
                if (respond.Status == PackageStatusCode.OK)
                {
                    HocKyDictionary[key] = respond.Respond;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
