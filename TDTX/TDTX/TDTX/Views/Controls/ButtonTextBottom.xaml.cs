using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace TDTX.Views.Controls
{
    public partial class ButtonTextBottom : Grid
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(ButtonTextBottom), null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(ButtonTextBottom), null);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(ButtonTextBottom), 10.0D);

        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(string), typeof(ButtonTextBottom), string.Empty);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(ButtonTextBottom), string.Empty);


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        public ButtonTextBottom()
        {
            InitializeComponent();
            this.BindingContext = this;

        }
    }
}
