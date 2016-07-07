using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using TDTX.Views.Extensions;

namespace TDTX.Views.Base
{
    public class TextProvider
    {
        static readonly string ResourceId = "TDTX.Portable.Resx.Dictionary";

        public static string Translate(string key)
        {
            if (key == null)
                return "";

            ResourceManager resmgr = new ResourceManager(ResourceId
                                , typeof(TranslateExtension).GetTypeInfo().Assembly);
            CultureInfo ci = new CultureInfo(Settings.Instance.Language);

            var translation = resmgr.GetString(key,
                ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", key, ResourceId, ci.Name));
#else
                translation = key; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;

        }

    }
}
