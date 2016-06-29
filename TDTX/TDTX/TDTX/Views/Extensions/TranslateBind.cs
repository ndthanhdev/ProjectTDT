using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    public class TranslateBind : IMarkupExtension
    {
        public Binding Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            BindableObject targetObject;
            BindableProperty targetProperty;
            if (target != null && target.TargetObject is BindableObject && target.TargetProperty is BindableProperty)
            {
                targetObject = (BindableObject)target.TargetObject;
                targetProperty = (BindableProperty)target.TargetProperty;
            }
            else
            {
                return this;
            }
            targetObject.SetBinding(targetProperty,Text.Path);
            var text = targetObject.GetValue(targetProperty);
            return text;
        }
        private static BindableProperty TextBindingSinkProperty = BindableProperty.CreateAttached(
            propertyName: "TextBindingSinkProperty",
            returnType: typeof(object),// set the desired type of Param1 for at least runtime type safety check
            declaringType: typeof(TranslateBind),
            defaultValue: null);
    }
}
