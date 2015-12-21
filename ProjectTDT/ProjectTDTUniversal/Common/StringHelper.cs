using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Common
{
    //all utility with string
    public class StringHelper
    {
        public static string RegexString(string content,params string[] patterns)
        {
            for(int i=0; i<patterns.Length &&!String.IsNullOrEmpty(content);i++)
            {
                Regex reg = new Regex(patterns[i]);
                content = reg.Match(content).Value;
            }
            return content;
        }
        public static IEnumerable<string> RegexStrings(string content, params string[] patterns)
        {
            List<string> result = new List<string>();
            List<string> temp = new List<string>();
            result.Add(content);
            for (int i=0;i<patterns.Length;i++)
            {
                Regex reg = new Regex(patterns[i]);
                foreach(string s in result)                
                    temp.AddRange(reg.Matches(s).Cast<Match>().Select(v => v.Value));
                result.Clear();
                result.AddRange(temp);
                temp.Clear();                
            }
            return result;
        }

        public static string MergeLine(string content)
        {
            return string.Join(" ",RegexStrings(content,TemplatesRegexPatterns.GetWord));
        }
    }
}
