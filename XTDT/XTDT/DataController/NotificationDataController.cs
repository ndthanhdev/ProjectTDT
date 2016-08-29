using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XTDT.API.Material;
using XTDT.API.Requests;
using XTDT.API.Respond;
using XTDT.API.ServiceAccess;
using XTDT.Common;
using XTDT.Models;

namespace XTDT.DataController
{
    public class NotificationDataController
    {
        private Dictionary<DonVi, ThongBaoItem> _tbDictionary;
        public Dictionary<DonVi, ThongBaoItem> TbDictionary
        {
            get { return _tbDictionary ?? (_tbDictionary = new Dictionary<DonVi, ThongBaoItem>()); }
            set { _tbDictionary = value; }
        }

        public async Task<bool> UpdateKeys(string id, string pass)
        {
            DSDonViRequest request = new DSDonViRequest() { user = id, pass = pass };
            var dsDV = await Transporter.Transport<DSDonViRequest, DSThongBao>(request);
            if (dsDV.Status != PackageStatusCode.OK)
                return false;
            foreach (var dv in dsDV.Respond.DonVi)
            {
                dv.Title = dv.Title.ClearLongWhiteSpace();
                TbDictionary[dv] = null;
            }
            return true;
        }
        public async Task<bool> UpdateValues()
        {
            return true;
        }
        public async Task<bool> ProvideValue(string id, string password, DonVi key)
        {
            var request = new DSThongBaoRequest()
            {
                user = id,
                pass = password,
                lv = key.Id,
                page = 0
            };
            var respond = await Transporter.Transport<DSThongBaoRequest, DSThongBao>(request);
            if (respond.Status != PackageStatusCode.OK)
                return false;
            return true;
        }
    }
}
