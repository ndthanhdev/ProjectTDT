using System;
using System.Collections.Generic;
using System.Text;
using TDTX.API;


namespace TDTX.API
{
    public class TransportRespond<T,U> where T : RequestObject
    {
        public T Request { get; set; }
        public U Respond { get; set; }
        public TransportStatusCode Status { get; set; }
    }

    public enum TransportStatusCode
    {
        Awaiting,
        NotAuthorized,
        Offline,
        OK,
        UnknownError
    }

}
