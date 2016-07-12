using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace TDTX.test
{
    class Class1 : IContractResolver
    {
        public JsonContract ResolveContract(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
