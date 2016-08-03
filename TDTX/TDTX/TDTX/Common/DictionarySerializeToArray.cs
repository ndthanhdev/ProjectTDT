using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Common
{
    [JsonArray]
    public class DictionarySerializeToArray<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }
}
