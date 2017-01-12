using KudaGo.Client.ViewModels.Details;
using KudaGo.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace KudaGo.Client.Views
{
    public sealed partial class MapDetailsControl : UserControl
    {
        public static readonly DependencyProperty CoordinatesProperty =
            DependencyProperty.Register("Coordinates", typeof(ICoordinates), typeof(MapDetailsControl), new PropertyMetadata(null, new PropertyChangedCallback(OnPropertyChanged)));

        public MapDetailsControl()
        {
            this.InitializeComponent();
        }

        public ICoordinates Coordinates
        {
            get { return (ICoordinates)GetValue(CoordinatesProperty); }
            set { SetValue(CoordinatesProperty, value); }
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapDetailsControl;
            if (control == null)
                return;

            var coords = e.NewValue as ICoordinates;
            if (coords == null)
                return;

            // Specify a known location.
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = coords.Lat, Longitude = coords.Lon };
            Geopoint snPoint = new Geopoint(snPosition);

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icons/map-marker.png"));
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.ZIndex = 0;

            // Add the MapIcon to the map.
            control.map.MapElements.Add(mapIcon1);

            // Center the map over the POI.
            BasicGeoposition centerPosition = new BasicGeoposition() { Latitude = coords.Lat - 0.0001, Longitude = coords.Lon };
            Geopoint centerPoint = new Geopoint(centerPosition);
            control.map.Center = centerPoint;
            control.map.ZoomLevel = 16;
        }
    }
}
