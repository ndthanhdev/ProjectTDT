using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TDTX.Base;

namespace TDTX.Models
{
    [JsonObject]
    public class Settings:ILocalObject
    {
        private static Settings _instance;
        public static Settings Instance => _instance ?? new Settings();
        public Settings()
        {
            Language = "en";
            _instance = this;
        }
        public string Language { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string FileName => App.SettingsFile;
    }
}
