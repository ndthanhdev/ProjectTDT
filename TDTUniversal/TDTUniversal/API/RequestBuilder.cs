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
    public class RequestBuilder
    {
        private string _host = "https://api.trautre.cf/v2.php?";
        private object _request;
        public RequestBuilder(object requestObject)
        {
            if (_request == null)
                throw new NullReferenceException();
            _request = requestObject;
        }
        public async Task<String> BuildQuery()
        {
            await Task.Yield();
            if(_request is IRequestWithToken)
            {

            }
            return ObjectToQuery();
        }
        private string ObjectToQuery()
        {
            var tp = _request.GetType();
            var properties = from pro in tp.GetProperties()
                             where !pro.IsDefined(typeof(RequestIgnoreAttribute))
                             select pro;
            StringBuilder sb = new StringBuilder();
            sb.Append(_host);
            foreach (var pro in properties)
            {
                if (pro.IsDefined(typeof(RequestParameterAttribute)))
                {
                    var attribute = (RequestParameterAttribute)pro.GetCustomAttribute(typeof(RequestParameterAttribute));
                    sb.Append($"{attribute.Name}={pro.GetValue(_request)?.ToString() ?? string.Empty}");
                }
                else
                    sb.Append($"{pro.Name}={pro.GetValue(_request)?.ToString() ?? string.Empty}");
            }
            return sb.ToString();
        }

        

    }
}
