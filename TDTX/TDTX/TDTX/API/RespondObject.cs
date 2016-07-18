using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.API
{
    public abstract class RespondObject<T> where T:RequestObject
    {
        public T Request { get; set; }

        public RespondObject()
        {
            
        }
        public RespondObject(T request)
        {
            Request = request;
        }

    }
}
