using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            var imageSource = Device.OnPlatform<ImageSource>(
                iOS:ImageSource.FromResource("TDTX.iOS."+ Source),
                 Android: ImageSource.FromResource("TDTX.Droid." + Source),
                  WinPhone: ImageSource.FromResource("TDTX.UWP."+ Source));
            
            return imageSource;
        }
    }
}
