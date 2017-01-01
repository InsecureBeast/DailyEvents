﻿using KudaGo.Client.Helpers;
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

namespace KudaGo.Client.Views
{
    public sealed partial class NavigationControl : UserControl
    {
        public static readonly DependencyProperty ViewContentProperty = 
            DependencyProperty.Register("ViewContent", typeof(object), typeof(NavigationControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NavigationControl), new PropertyMetadata(null));


        public NavigationControl()
        {
            InitializeComponent();
            var device = DeviceTypeHelper.GetDeviceFormFactorType();
            if (device == DeviceFormFactorType.Phone || device == DeviceFormFactorType.IoT || device == DeviceFormFactorType.Other)
                splitView.CompactPaneLength = 0;

            Loaded += NavigationControl_Loaded;
        }

        private void NavigationControl_Loaded(object sender, RoutedEventArgs e)
        {
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("ViewContent"), Source = this });

            if (Title != null)
                title.Text = Title;
        }

        public object ViewContent
        {
            get { return GetValue(ViewContentProperty); }
            set { SetValue(ViewContentProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }
    }
}
