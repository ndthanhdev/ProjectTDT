using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using TDTX.ViewModels;
using TDTX.Views.Extensions.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    // refer wpf http://stackoverflow.com/questions/10328802/markupextension-with-binding-parameters
    // refer https://forums.xamarin.com/discussion/36884/missing-implementation-of-iprovidevaluetarget-targetproperty-property-imarkupextension
    public class TranslateBind : BindableObject, IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "TDTX.Portable.Resx.Dictionary";
        private readonly BindableProperty pro = BindableProperty.CreateAttached("Pro",typeof(object),typeof(TranslateBind),"getting..");
        public Binding Text { get; set; }
        private string _propertyName = "";
        public TranslateBind()
        {
            ci = new System.Globalization.CultureInfo("vi");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text is Binding)
            {
                IProvideValueTarget target =
                    serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                BindableObject targetObject = (BindableObject) target.TargetObject;
                targetObject.SetBinding(pro,Text);
                var v= targetObject.GetValue(pro);
                //targetObject.BindingContextChanged += TargetObject_BindingContextChanged;
                //targetObject.PropertyChanging += TargetObject_PropertyChanging;
                return "getting...";
            }
            return "error load";
        }

        private void TargetObject_BindingContextChanged(object sender, EventArgs e)
        {
            BindableObject targetObject = (BindableObject)sender;
            BindableProperty targetProperty = BindableProperty.Create("Text", typeof(string), targetObject.GetType());
            this.BindingContext = targetObject.BindingContext;
            this.SetBinding(pro,Text);
            ApplyBindings();
            ResourceManager resmgr = new ResourceManager(ResourceId
                                , typeof(TranslateExtension).GetTypeInfo().Assembly);
            var temp = (string) targetObject.GetValue(pro);
           
            var translation = resmgr.GetString((string)targetObject.GetValue(targetProperty), ci);

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
            targetObject.SetValue(targetProperty, translation);
        }

        private string GetPropertyName()
        {
            
        }


        //public object ProvideValue(IServiceProvider serviceProvider)
        //{

        //    var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

        //    var property = provideValueTarget.GetType().GetPropertyEx("Node");
        //    var xamlNode = property.GetValue(provideValueTarget);

        //    property = xamlNode.GetType().GetPropertyEx("Parent");
        //    var parentNode = property.GetValue(xamlNode);

        //    property = parentNode.GetType().GetPropertyEx("Properties");
        //    var properties = (IDictionary)property.GetValue(parentNode);

        //    object xmlName = null;
        //    foreach (DictionaryEntry entry in properties)
        //    {
        //        if (ReferenceEquals(entry.Value, xamlNode))
        //        {
        //            xmlName = entry.Key;
        //            break;
        //        }
        //    }

        //    property = xmlName.GetType().GetPropertyEx("LocalName");
        //    var propertyName = (string)property.GetValue(xmlName);

        //    var target = provideValueTarget.TargetObject;
        //    var targetProperty = target.GetType().GetPropertyEx(propertyName);
        //    BindableObject b = (BindableObject)provideValueTarget.TargetObject;

        //    BindableProperty bp = BindableProperty.Create(targetProperty.Name, typeof(string), typeof(TranslateBind));
        //    b.SetBinding(bp, "Title");


        //    var v = b.GetValue(bp);
        //    return null;
        //}

        //    private static BindableProperty TextBindingSinkProperty = BindableProperty.CreateAttached(
        //        propertyName: "TextBindingSinkProperty",
        //        returnType: typeof(object),// set the desired type of Param1 for at least runtime type safety check
        //        declaringType: typeof(TranslateBind),
        //        defaultValue: null);
        //}
    }
}
