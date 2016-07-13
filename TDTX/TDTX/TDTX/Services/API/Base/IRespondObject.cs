using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Services.API.Base
{
    public interface IRespondObject
    {
        RequestObject Request { get; }

    }
}
