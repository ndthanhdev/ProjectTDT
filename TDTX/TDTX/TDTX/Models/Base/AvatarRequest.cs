using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models.Base
{
    public class AvatarRequest : IRequestObject
    {
        public Type RespondType
        {
            get { return typeof(Avatar); }
        }
    }
}
