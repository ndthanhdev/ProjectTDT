using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private Dictionary<DonVi, ObservableCollection<ThongBaoItem>> _tbDictionary;
        public Dictionary<DonVi, ObservableCollection<ThongBaoItem>> TbDictionary
        {
            get { return _tbDictionary ?? (_tbDictionary = new Dictionary<DonVi, ObservableCollection<ThongBaoItem>>()); }
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
                TbDictionary[dv] = new ObservableCollection<ThongBaoItem>();
            }
            return true;
        }
        public async Task<bool> UpdateValues(string id, string pass)
        {
            foreach (var key in TbDictionary.Keys)
            {
                await ProvideValue(id, pass, key);
            }
            return true;
        }
        public async Task<bool> ProvideValue(string id, string password, DonVi key)
        {
            var request = new DSThongBaoRequest()
            {
                user = id,
                pass = password,
                lv = key.Id,
                page = 1
            };
            var respond = await Transporter.Transport<DSThongBaoRequest, DSThongBao>(request);
            if (respond.Status != PackageStatusCode.OK)
                return false;
            List<ThongBao> listTb = new List<ThongBao>();
            foreach (var tb in respond.Respond.Thongbao)
                listTb.Add(tb);
            List<Task<Package<DSThongBaoRequest, DSThongBao>>> tasks = new List<Task<Package<DSThongBaoRequest, DSThongBao>>>();
            for (int i = 2; i < respond.Respond.Numpage; i++)
            {
                tasks.Add(Transporter.Transport<DSThongBaoRequest, DSThongBao>(new DSThongBaoRequest()
                {
                    user = id,
                    pass = password,
                    lv = key.Id,
                    page = i
                }));
            }
            await Task.WhenAll(tasks);
            foreach (var t in tasks)
            {
                if (t.Result.Status != PackageStatusCode.OK)
                    continue;
                foreach (var tb in t.Result.Respond.Thongbao)
                    listTb.Add(tb);
            }
            listTb.Sort();
            listTb.Reverse();
            List<ThongBaoItem> listTbi = new List<ThongBaoItem>();
            listTbi.AddRange(from tb in listTb select new ThongBaoItem(tb));
            foreach (var tbi in listTbi)
                TbDictionary[key].Add(tbi);
            return true;
        }
    }
}
