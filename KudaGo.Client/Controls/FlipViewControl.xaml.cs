﻿using System;
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

namespace DailyEvents.Client.Controls
{
    public sealed partial class FlipViewControl : UserControl
    {
        public FlipViewControl()
        {
            this.InitializeComponent();
        }

        private void layoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            flipView.Height = layoutRoot.ActualWidth / 1.5;
        }
    }
}
