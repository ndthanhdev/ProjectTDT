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
        private Uri _link;

        private string[] attributes;

        public static bool Match(HttpForm form,params string[] args)
        {
            return form.Attributes.Length.Equals(args.Length);
        }

        public string[] Attributes
        {
            get { return attributes; }
        }

        public Uri Link
        {
            get { return _link; }
            private set
            {
                _link = value;
            }
        }

        public HttpForm(Uri Link , params string[] Attributes)
        {
            this.Link = Link;
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
