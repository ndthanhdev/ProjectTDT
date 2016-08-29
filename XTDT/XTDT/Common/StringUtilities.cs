using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XTDT.Common
{
    public static class StringUtilities
    {
        public static string ClearLongWhiteSpace(this string s)
        {
            return Regex.Replace(s.Trim(), @"\s{2,}", " ");
        }
    }
}
