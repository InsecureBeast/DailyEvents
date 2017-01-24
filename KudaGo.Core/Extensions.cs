using System;
using System.Text;
using KudaGo.Core.Search;

namespace KudaGo.Core
{
    static class Extensions
    {
        public static string GetLocation(this Location location)
        {
            switch (location)
            {
                case Location.Spb:
                    return "spb";
                case Location.Moskva:
                    return "msk";
                case Location.Novosibirsk:
                    return "nsk";
                case Location.Ekaterinburg:
                    return "ekb";
                case Location.NNovgorod:
                    return "nnv";
                case Location.Kazan:
                    return "kzn";
                case Location.Viborg:
                    return "vbg";
                case Location.Samara:
                    return "smr";
                case Location.Krasnodar:
                    return "krd";
                case Location.Sochi:
                    return "sochi";
                case Location.Ufa:
                    return "ufa";
                case Location.Krasnoyarsk:
                    return "krasnoyarsk";
                case Location.Kiev:
                    return "kev";
                case Location.NewYork:
                    return "new-york";
                default:
                    throw new ArgumentOutOfRangeException("location", location, null);
            }
        }

        public static string GetCType(this CType cType)
        {
            switch (cType)
            {
                case CType.News:
                    return "news";
                case CType.Event:
                    return "event";
                case CType.Place:
                    return "place";
                case CType.List:
                    return "list";
                default:
                    throw new ArgumentOutOfRangeException("cType", cType, null);
            }
        }

        public static CType GetCType(this string ctype)
        {
            switch (ctype)
            {
                case "news":
                    return CType.News;
                case "event":
                    return CType.Event;
                case "place":
                    return CType.Place;
                case "list":
                    return CType.List;
                default:
                    throw new NotSupportedException("ctype with name " + ctype + "does not support");
            }
        }
    }
}
