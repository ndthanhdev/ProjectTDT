using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TDTX.Views.Base
{
    public class ImageProvider
    {
        public static ImageSource GetImageResource(string source)
        {
            if (source == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(App.NameSpace + source);

            return imageSource;
        }
    }
}
