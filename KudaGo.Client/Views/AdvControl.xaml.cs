using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace DailyEvents.Client.Views
{
    public enum AdvSize
    {
        Big,
        Small
    }

    public sealed partial class AdvControl : UserControl
    {
        public static readonly DependencyProperty SizeProperty =
           DependencyProperty.Register("Size", typeof(AdvSize), typeof(GridViewControl), new PropertyMetadata(AdvSize.Big, OnSizePropertyChanged));

        public AdvControl()
        {
            this.InitializeComponent();
            AdvPresenter.Content = CreateBigBanner();
        }

        public AdvSize Size
        {
            get { return (AdvSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AdvControl;
            if (control == null)
                return;

            var value = (AdvSize)e.NewValue;
            switch (value)
            {
                case AdvSize.Big:
                    control.AdvPresenter.Content = CreateBigBanner();
                    break;
                case AdvSize.Small:
                    control.AdvPresenter.Content = CreateSmallBanner();
                    break;
                default:
                    break;
            }
        }

        private static AdControl CreateBigBanner()
        {
            // Programatically create an ad control. This must be done from the UI thread.
            var adControl = new AdControl();

            // Set the application id and ad unit id
            // The application id and ad unit id can be obtained from Dev Center.
            // See "Monetize with Ads" at https://msdn.microsoft.com/en-us/library/windows/apps/mt170658.aspx
            adControl.ApplicationId = "d25517cb-12d4-4699-8bdc-52040c712cab";
            adControl.AdUnitId = "10043121";
            //adControl.ApplicationId = "d0d564f3-1868-4420-92a3-20e95e0ff118";
            //adControl.AdUnitId = "11671507";

            // Set the dimensions
            adControl.Width = 300;
            adControl.Height = 250;

            // Add event handlers if you want
            adControl.ErrorOccurred += OnErrorOccurred;
            adControl.AdRefreshed += OnAdRefreshed;

            return adControl;
        }

        private static AdControl CreateSmallBanner()
        {
            // Programatically create an ad control. This must be done from the UI thread.
            var adControl = new AdControl();

            // Set the application id and ad unit id
            // The application id and ad unit id can be obtained from Dev Center.
            // See "Monetize with Ads" at https://msdn.microsoft.com/en-us/library/windows/apps/mt170658.aspx
            adControl.ApplicationId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
            adControl.AdUnitId = "10865270";
            //adControl.ApplicationId = "d0d564f3-1868-4420-92a3-20e95e0ff118";
            //adControl.AdUnitId = "11671510";

            // Set the dimensions
            adControl.Width = 320;
            adControl.Height = 50;

            // Add event handlers if you want
            adControl.ErrorOccurred += OnErrorOccurred;
            adControl.AdRefreshed += OnAdRefreshed;

            return adControl;
        }

        private static void OnAdRefreshed(object sender, RoutedEventArgs e)
        {
        }

        private static void OnErrorOccurred(object sender, AdErrorEventArgs e)
        {
        }
    }
}
