using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Base
{
    [JsonArray]
    public class DictionarySerializeToArray<TKey, TValue> : Dictionary<TKey, TValue>
    {

    }
}
