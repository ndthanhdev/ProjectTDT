using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace TDTUniversal.Controls
{
    [TemplatePart(Name = PartScroller, Type = typeof(ScrollViewer))]
    public sealed class PullToRefreshListViewEx : Microsoft.Toolkit.Uwp.UI.Controls.PullToRefreshListView
    {

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadMoreCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadMoreCommandProperty =
            DependencyProperty.Register("LoadMoreCommand", typeof(ICommand), typeof(PullToRefreshListViewEx), new PropertyMetadata(null));

        /// <summary>
        /// Occurs when the user has requested content to be refreshed
        /// </summary>
        public event EventHandler LoadMoreRequested;

        private const string PartScroller = "ScrollViewer";

        private ScrollViewer _scroller;

        public PullToRefreshListViewEx()
        {
            this.DefaultStyleKey = typeof(PullToRefreshListViewEx);
        }

        protected override void OnApplyTemplate()
        {
            _scroller = GetTemplateChild(PartScroller) as ScrollViewer;
            if (_scroller != null)
            {
                _scroller.ViewChanged += _scroller_ViewChanged;
            }
            base.OnApplyTemplate();
        }

        private void _scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (PullThreshold > _scroller.ScrollableHeight - _scroller.VerticalOffset)
            {
                LoadMoreRequested?.Invoke(this, new EventArgs());
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                {
                    LoadMoreCommand.Execute(null);
                }
            }
        }
    }
}
