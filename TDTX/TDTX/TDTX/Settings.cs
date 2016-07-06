using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX
{
    [JsonObject]
    public class Settings
    {
        private static Settings _current;
        public static Settings Current => _current ?? new Settings();
        public Settings()
        {
            Language = "en";
            _current = this;
        }
        public string Language { get; set; }
    }
}
