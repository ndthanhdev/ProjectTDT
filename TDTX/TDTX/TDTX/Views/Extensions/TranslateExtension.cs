using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using TDTX.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public TranslateExtension()
        {
        }

        public string Text
        {
            get;
            set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return TextProvider.Translate(Text);
        }
    }
}
