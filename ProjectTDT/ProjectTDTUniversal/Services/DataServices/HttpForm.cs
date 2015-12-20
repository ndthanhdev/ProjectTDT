using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ProjectTDTUniversal.Services.DataServices
{
    public class HttpForm
    {
        private string _link;

        private string[] attributes;

        public static bool Match(HttpForm form,params string[] args)
        {
            return form.Attributes.Length.Equals(args.Length);
        }

        public string[] Attributes
        {
            get { return attributes; }
        }

        public string Path
        {
            get { return _link; }
        }
        public Uri Link
        {
            get { return new Uri(_link); }
        }

        public HttpForm(string Link , params string[] Attributes)
        {
            this._link = Link;
            this.attributes = Attributes;
        }

        public int NumberOfAttributes
        {
            get { return attributes.Length; }
        }

        public HttpMethod Method
        {
            get
            {
                if (NumberOfAttributes > 0)
                    return HttpMethod.Post;
                return HttpMethod.Get;
            }
        }
             

        public HttpFormUrlEncodedContent FillIn(params string[] args)
        {           
            if (!Match(this, args))
                return null;
            Dictionary<string, string> data = new Dictionary<string, string>();
            for (int i = 0; i < NumberOfAttributes; i++)
                data.Add(Attributes[i], args[i]);
            return new HttpFormUrlEncodedContent(data);
        }
    }
}
