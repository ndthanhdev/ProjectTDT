using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using TDTX.Views.Extensions;

namespace TDTX.Views.Base
{
    public class TranslateBase
    {
        private static readonly CultureInfo Ci =  new System.Globalization.CultureInfo("vi");
        static readonly string ResourceId = "TDTX.Portable.Resx.Dictionary";

        public static string Translate(string key)
        {
            if (key == null)
                return "";

            ResourceManager resmgr = new ResourceManager(ResourceId
                                , typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resmgr.GetString(key, Ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", key, ResourceId, Ci.Name));
#else
                translation = key; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;

        }

    }
}
