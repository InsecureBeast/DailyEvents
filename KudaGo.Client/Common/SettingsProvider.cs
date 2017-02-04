using KudaGo.Core;
using KudaGo.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace KudaGo.Client.Common
{
    interface ISettingsProvider
    {
        void SaveLocation(Location location);
        Location GetLocation();
    }

    class SettingsProvider : ISettingsProvider
    {
        private ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        public Location GetLocation()
        {
            var value = _settings.Values["Location"] as string;
            if (value == null)
                return Location.Moskva;

            return value.GetLocation();
        }

        public void SaveLocation(Location location)
        {
            // Create a simple setting
            var str = location.GetStrLocation();

            // Delete a simple setting
            //_settings.Values.Remove("Location");

            _settings.Values["Location"] = str;
        }
    }
}
