using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Views.Base;
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
            return ImageProvider.GetImageResource(Source);
        }
    }
}
