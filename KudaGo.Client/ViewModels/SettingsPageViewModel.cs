using KudaGo.Client.Command;
using KudaGo.Client.Common;
using KudaGo.Client.Model;
using KudaGo.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Helpers;

namespace KudaGo.Client.ViewModels
{
    class SettingsPageViewModel : PropertyChangedBase
    {
        private readonly IDataSource _dataSource;
        private readonly List<LocationItem> _locations;
        private readonly ISettingsChangeNotifier _notifier;
        private readonly ISettingsProvider _settingsProvider;
        private LocationItem _selectedLocation;

        public SettingsPageViewModel(IDataSource dataSource, ISettingsChangeNotifier notifier, ISettingsProvider settingsProvider)
        {
            _dataSource = dataSource;
            _notifier = notifier;
            _settingsProvider = settingsProvider;

            _locations = new List<LocationItem>();
            LoadLocations();

            //From settings
            var savedLocation = _settingsProvider.GetLocation();
            var selected = _locations.FirstOrDefault(l => l.Location == savedLocation);
            SelectedLocation = selected;
        }

        public IEnumerable<LocationItem> Locations => _locations;
        public LocationItem SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                NotifyOfPropertyChanged(() => SelectedLocation);
                Save();
            }
        }

        private void LoadLocations()
        {
            foreach (Location location in Enum.GetValues(typeof(Location)))
            {
                _locations.Add(new LocationItem(location));
            }
        }

        private void Save()
        {
            _dataSource.SetLocation(SelectedLocation.Location);
            //save to settings
            _settingsProvider.SaveLocation(SelectedLocation.Location);
            _notifier.Notify();
        }
    }

    class LocationItem
    {
        public LocationItem(Location location)
        {
            Location = location;
            Title = LocationHelper.GetCity(location);
        }

        public Location Location { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
