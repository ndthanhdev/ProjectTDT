using System;
using System.Collections.Generic;
using System.Text;
using XTDT.API.Requests;

namespace XTDT.API.Material
{
    public class Package<T, U> where T : RequestObject
    {
        public T Request { get; set; }
        public U Respond { get; set; }
        public PackageStatusCode Status { get; set; }
    }
    public enum PackageStatusCode
    {
        Awaiting,
        NotAuthorized,
        Offline,
        OK,
        UnknownError
    }
}
