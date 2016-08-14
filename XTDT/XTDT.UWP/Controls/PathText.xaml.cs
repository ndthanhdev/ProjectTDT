using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace XTDT.UWP.Controls
{
    public sealed partial class PathText : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(PathText), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(PathText), new PropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler PropertyChanged;

        public PathText()
        {
            this.InitializeComponent();
            DataContext = this;
        }
        public Geometry Path { get { return path.Data; } set { path.Data = value; } }
        public string Text { get { return text.Text; } set { text.Text = value; } }

        public bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)

        {

            if (object.Equals(storage, value))

                return false;



            storage = value;

            this.RaisePropertyChanged(propertyName);

            return true;

        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)

        {

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)

                return;



            var handler = PropertyChanged;

            if (!object.Equals(handler, null))

            {

                var args = new PropertyChangedEventArgs(propertyName);

                var dispatcher = WindowWrapper.Current().Dispatcher;

                if (dispatcher.HasThreadAccess())
                {

                    try

                    {

                        handler.Invoke(this, args);

                    }

                    catch

                    {

                        dispatcher.Dispatch(() => handler.Invoke(this, args));

                    }

                }

                else

                {

                    dispatcher.Dispatch(() => handler.Invoke(this, args));

                }

            }

        }
    }
}
