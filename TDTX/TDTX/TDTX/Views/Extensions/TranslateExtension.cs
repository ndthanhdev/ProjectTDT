using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "TDTX.Portable.Resx.Dictionary";

        //public static BindableProperty TextProperty = BindableProperty.CreateAttached(
        //    propertyName: "Text",
        //    returnType: typeof(string),
        //    declaringType: typeof(TranslateExtension),
        //    defaultValue: null,
        //    defaultBindingMode: BindingMode.TwoWay);

        public TranslateExtension()
        {
            ci = new System.Globalization.CultureInfo("vi");
            //this.Text = key;
        }

        public string Text
        {
            get;//{ return (string)GetValue(TextProperty); }
            set;// { SetValue(TextProperty, value);}
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resmgr = new ResourceManager(ResourceId
                                , typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resmgr.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
