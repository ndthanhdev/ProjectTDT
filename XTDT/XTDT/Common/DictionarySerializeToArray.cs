using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.Common
{
    [JsonArray]
    public class DictionarySerializeToArray<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }
}
