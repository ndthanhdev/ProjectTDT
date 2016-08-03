using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TDTX.Common
{
    public class FileImageProvider
    {
        public static FileImageSource GetFileImageSource(string source)
        {
            if (source == null)
            {
                return null;
            }
            var imageSource = FileImageSource.FromFile(App.NameSpace + "." + source);

            return (FileImageSource)imageSource;
        }
    }
}
