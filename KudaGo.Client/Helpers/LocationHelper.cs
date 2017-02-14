using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Search;

namespace KudaGo.Client.Helpers
{
    class LocationHelper
    {
        public static string GetCity(Location location)
        {
            switch (location)
            {
                case Location.Spb:
                    return "Санкт-Петербург";
                case Location.Moskva:
                    return "Москва";
                case Location.Novosibirsk:
                    return "Новосибирск";
                case Location.Ekaterinburg:
                    return "Екатеринбург";
                case Location.NNovgorod:
                    return "Нижний Новгород";
                case Location.Kazan:
                    return "Казань";
                case Location.Viborg:
                    return "Выборг";
                case Location.Samara:
                    return "Самара";
                case Location.Krasnodar:
                    return "Краснодар";
                case Location.Sochi:
                    return "Сочи";
                case Location.Ufa:
                    return "Уфа";
                case Location.Krasnoyarsk:
                    return "Красноярск";
                case Location.Kiev:
                    return "Киев";
                case Location.NewYork:
                    return "Нью-Йорк";
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }
    }
}
