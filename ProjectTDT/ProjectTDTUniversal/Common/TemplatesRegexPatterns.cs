using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Common
{
    public static class TemplatesRegexPatterns
    {
        // get user's full name from student page
        public const string GetUserFullName= "(?<=class=\"student-info\">)(.|\\n)*?(?=<)";
        public const string GetWord = "(?:\\S)+";
        public const string GetNotify= "<a href=\"\\/thongbao\\/(.|\n)+?<\\/a>";
        public const string GetPtag = "<p(.|\\s)+?</p>";
        public const string HtmlStrip = "<[^>]+>";
    }
}
