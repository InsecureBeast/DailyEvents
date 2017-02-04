using KudaGo.Client.Command;
using KudaGo.Client.Common;
using KudaGo.Client.Model;
using KudaGo.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _locations.Add(new LocationItem(Location.Ekaterinburg, "Екатеринбург"));
            _locations.Add(new LocationItem(Location.Kazan, "Казань"));
            _locations.Add(new LocationItem(Location.Kiev, "Киев"));
            _locations.Add(new LocationItem(Location.Krasnodar, "Краснодар"));
            _locations.Add(new LocationItem(Location.Krasnoyarsk, "Красноярск"));
            _locations.Add(new LocationItem(Location.Moskva, "Москва"));
            _locations.Add(new LocationItem(Location.NewYork, "Нью-Йорк"));
            _locations.Add(new LocationItem(Location.NNovgorod, "Нижний Новгород"));
            _locations.Add(new LocationItem(Location.Novosibirsk, "Новосибирск"));
            _locations.Add(new LocationItem(Location.Samara, "Самара"));
            _locations.Add(new LocationItem(Location.Sochi, "Сочи"));
            _locations.Add(new LocationItem(Location.Spb, "Санкт-Петербург"));
            _locations.Add(new LocationItem(Location.Ufa, "Уфа"));
            _locations.Add(new LocationItem(Location.Viborg, "Выборг"));
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
        public LocationItem(Location location, string localizeName)
        {
            Location = location;
            Title = localizeName;
        }

        public Location Location { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
