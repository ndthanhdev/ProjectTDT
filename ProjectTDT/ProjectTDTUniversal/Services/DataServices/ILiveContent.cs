using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Services.DataServices
{
    public interface ILiveContent
    {
        ///// <summary>
        ///// to confirm filename on local disk
        ///// </summary>
        //string Prefix { get; }

        //Task UpdateContents();

        KeyValuePair<string,Type>[] Properties { get; }

    }
}
