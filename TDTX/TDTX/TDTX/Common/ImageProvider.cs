using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TDTX.Common
{
    public class ImageProvider
    {
        public static ImageSource GetImageResource(string source)
        {
            if (source == null)
            {
                return null;
            }
            var imageSource = ImageSource.FromResource(App.NameSpace + "." + source);

            return imageSource;
        }
    }
}
