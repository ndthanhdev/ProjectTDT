using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Models.Base
{
    interface IRequestObject
    {
        
        Type RespondType { get; }
    }
}
