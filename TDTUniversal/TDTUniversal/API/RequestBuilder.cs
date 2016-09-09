using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API
{
    public static class RequestBuilder
    {
        private static string _host = "https://api.trautre.cf/v2.php";
        public static async Task<String> BuildUrl(object requestObject, TokenProvider tokenProvider = null)
        {
            if (requestObject == null)
                throw new NullReferenceException();
            if (requestObject is IRequestWithToken)
            {
                var tokenTask = tokenProvider.GetTokenAsync();
                var rawUrl = GenerateUrl(requestObject);
                await tokenTask;
                return $"{rawUrl}&token={tokenTask.Result}";
            }
            return GenerateUrl(requestObject);
        }
        private static string GenerateUrl(object requestObject)
        {
            var tp = requestObject.GetType();
            var properties = from pro in tp.GetProperties()
                             where !pro.IsDefined(typeof(RequestIgnoreAttribute))
                             select pro;
            List<string> ls = new List<string>();
            foreach (var pro in properties)
            {
                if (pro.IsDefined(typeof(RequestParameterAttribute)))
                {
                    var attribute = (RequestParameterAttribute)pro.GetCustomAttribute(typeof(RequestParameterAttribute));
                    ls.Add($"{attribute.Name}={pro.GetValue(requestObject)?.ToString() ?? string.Empty}");
                }
                else
                    ls.Add($"{pro.Name}={pro.GetValue(requestObject)?.ToString() ?? string.Empty}");
            }
            var query = string.Join("&", ls);
            return $"{_host}?{query}";
        }



    }
}
