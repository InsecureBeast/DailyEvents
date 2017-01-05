using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KudaGo.Client.Extensions
{
    class WebViewExtensions
    {
    }

    // <summary>
    /// Ads auto-hyperlinking to a RichTextBlock control.
    /// </summary>
    public static class HyperlinkExtensions
    {
        /// <summary>
        /// The raw text property.
        /// </summary>
        public static readonly DependencyProperty RawTextProperty =
            DependencyProperty.RegisterAttached("RawText", typeof(string), typeof(RichTextBlock), new PropertyMetadata("", OnRawTextChanged));

        /// <summary>
        /// Gets the raw text.
        /// </summary>
        public static string GetRawText(DependencyObject obj)
        {
            return (string)obj.GetValue(RawTextProperty);
        }

        /// <summary>
        /// Sets the raw text.
        /// </summary>
        public static void SetRawText(DependencyObject obj, string value)
        {
            obj.SetValue(RawTextProperty, value);
        }

        /// <summary>
        /// Called when raw text changed.
        /// </summary>
        private static void OnRawTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Transformation logic ...
        }
    }
}
