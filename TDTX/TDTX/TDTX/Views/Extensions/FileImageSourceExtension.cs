using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    [ContentProperty("Source")]
    public class FileImageSourceExtension:IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Views.Base.FileImageProvider.GetFileImageSource(Source);
        }
    }
}
