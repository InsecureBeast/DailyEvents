using DailyEvents.Client.Helpers;
using DailyEvents.Client.ViewModels.Details;
using DailyEvents.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace DailyEvents.Client.Views
{
    public sealed partial class MapDetailsControl : UserControl
    {
        public static readonly DependencyProperty CoordinatesProperty =
            DependencyProperty.Register("Coordinates", typeof(ICoordinates), typeof(MapDetailsControl), new PropertyMetadata(null, new PropertyChangedCallback(OnPropertyChanged)));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(MapDetailsControl), new PropertyMetadata(false, new PropertyChangedCallback(OnReadOnlyChanged)));

        private MapIcon _mapCurrentLocationIcon;
        private static Geopoint _targetPoint;

        public MapDetailsControl()
        {
            this.InitializeComponent();

            if (DeviceTypeHelper.GetDeviceFormFactorType() != DeviceFormFactorType.Phone &&
                DeviceTypeHelper.GetDeviceFormFactorType() != DeviceFormFactorType.Tablet)
                CurrentLocation.Visibility = Visibility.Collapsed; 

            _mapCurrentLocationIcon = new MapIcon();
            _mapCurrentLocationIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icons/curlocation-marker.png"));
        }

        public ICoordinates Coordinates
        {
            get { return (ICoordinates)GetValue(CoordinatesProperty); }
            set { SetValue(CoordinatesProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        private async Task GoToCurrentLocation()
        {
            try
            {
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();

                switch (accessStatus)
                {
                    case GeolocationAccessStatus.Allowed:

                        // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                        Geolocator geolocator = new Geolocator();// { DesiredAccuracyInMeters = _desireAccuracyInMetersValue };

                        // Carry out the operation
                        Geoposition pos = await geolocator.GetGeopositionAsync().AsTask();//token);
                        Geopoint snPoint = pos.Coordinate.Point;//new Geopoint(pos);

                        // Create a Current location icon.
                        _mapCurrentLocationIcon.Location = snPoint;
                        
                        // Add the Current location icon to the map.
                        if (!map.MapElements.Contains(_mapCurrentLocationIcon))
                            map.MapElements.Add(_mapCurrentLocationIcon);

                        // Center the map over the POI.
                        await map.TrySetViewAsync(snPoint);
                        break;

                    case GeolocationAccessStatus.Denied:
                        break;

                    case GeolocationAccessStatus.Unspecified:
                        break;
                }
            }
            catch (TaskCanceledException)
            {
                //_rootPage.NotifyUser("Canceled.", NotifyType.StatusMessage);
            }
            catch (Exception ex)
            {
                //_rootPage.NotifyUser(ex.ToString(), NotifyType.ErrorMessage);
            }
            finally
            {
                //_cts = null;
            }
        }

        private static bool MapIconExists(MapElement element)
        {
            var el = element as MapIcon;
            if (el != null && el.Title == "Текущее положение") //TODO 
                return true;
            return false;
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
            _targetPoint = new Geopoint(snPosition);

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icons/map-marker.png"));
            mapIcon1.Location = _targetPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            //mapIcon1.ZIndex = 0;

            // Add the MapIcon to the map.
            control.map.MapElements.Add(mapIcon1);

            // Center the map over the POI.
            BasicGeoposition centerPosition = new BasicGeoposition() { Latitude = coords.Lat - 0.0001, Longitude = coords.Lon };
            Geopoint centerPoint = new Geopoint(centerPosition);
            control.map.Center = centerPoint;
            control.map.ZoomLevel = 16;
        }

        private static void OnReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapDetailsControl;
            if (control == null)
                return;

            var isReadOnly = (bool)e.NewValue;
            if (isReadOnly)
            {
                control.BlockBorder.Visibility = Visibility.Visible;
                control.Controls.Visibility = Visibility.Collapsed;
            }
            else
            {
                control.BlockBorder.Visibility = Visibility.Collapsed;
                control.Controls.Visibility = Visibility.Visible;
            }
        }

        private async void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            await map.TryZoomInAsync();
        }

        private async void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            await map.TryZoomOutAsync();
        }

        private async void CurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            CurrentLocation.IsEnabled = false;
            await GoToCurrentLocation();
            CurrentLocation.IsEnabled = true;
            CurrentLocation.Visibility = Visibility.Collapsed;
            TargetLocation.Visibility = Visibility.Visible;
        }

        private async void TargetLocation_Click(object sender, RoutedEventArgs e)
        {
            TargetLocation.IsEnabled = false;
            await map.TrySetViewAsync(_targetPoint);
            TargetLocation.IsEnabled = true;
            CurrentLocation.Visibility = Visibility.Visible;
            TargetLocation.Visibility = Visibility.Collapsed;
        }
    }
}
