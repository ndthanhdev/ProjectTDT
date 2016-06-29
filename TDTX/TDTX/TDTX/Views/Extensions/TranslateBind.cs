using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Views.Extensions.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDTX.Views.Extensions
{
    // refer wpf http://stackoverflow.com/questions/10328802/markupextension-with-binding-parameters
    // refer https://forums.xamarin.com/discussion/36884/missing-implementation-of-iprovidevaluetarget-targetproperty-property-imarkupextension
    //public class TranslateBind : IMarkupExtension
    //{
    //    public Binding Text { get; set; }

    //    public object ProvideValue(IServiceProvider serviceProvider)
    //    {
            
    //        IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
    //        BindableObject targetObject;


    //        //BindableProperty targetProperty;
    //        var targetProperty = target.GetType().GetPropertyEx("Text");
    //        targetProperty.
    //        if (target != null && target.TargetObject is BindableObject && target.TargetProperty is BindableProperty)
    //        {
    //            targetObject = (BindableObject)target.TargetObject;
    //            targetProperty = (BindableProperty)target.TargetProperty;
    //        }
    //        else
    //        {
    //            return this;
    //        }
    //        targetObject.SetBinding(targetProperty,Text.Path);
    //        var text = targetObject.GetValue(targetProperty);
    //        return text;
    //    }
    //    private static BindableProperty TextBindingSinkProperty = BindableProperty.CreateAttached(
    //        propertyName: "TextBindingSinkProperty",
    //        returnType: typeof(object),// set the desired type of Param1 for at least runtime type safety check
    //        declaringType: typeof(TranslateBind),
    //        defaultValue: null);
    //}
}
