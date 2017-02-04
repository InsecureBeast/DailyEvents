using System;
using System.Text;
using KudaGo.Core.Search;

namespace KudaGo.Core
{
    public static class Extensions
    {
        public static string GetStrLocation(this Location location)
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

        public static Location GetLocation(this string strLocation)
        {
            switch (strLocation)
            {
                case "spb" :
                    return Location.Spb;
                case "msk" :
                    return Location.Moskva;
                case "nsk" :
                    return Location.Novosibirsk;
                case "ekb" :
                    return Location.Ekaterinburg;
                case "nnv" :
                    return Location.NNovgorod;
                case "kzn" :
                    return Location.Kazan;
                case "vbg" :
                    return Location.Viborg;
                case "smr" :
                    return Location.Samara;
                case "krd" :
                    return Location.Krasnodar;
                case "sochi":
                    return Location.Sochi;
                case "ufa" :
                    return Location.Ufa;
                case "krasnoyarsk" :
                    return Location.Krasnoyarsk;
                case "kev":
                    return Location.Kiev;
                case "new-york":
                    return Location.NewYork;
                default:
                    throw new ArgumentOutOfRangeException("location", strLocation, null);
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
                case CType.Movie:
                    return "movie";
                case CType.ListItem:
                    return "listitem";
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
                case "movie":
                    return CType.Movie;
                case "listitem":
                    return CType.ListItem;
                default:
                    throw new NotSupportedException("ctype with name " + ctype + "does not support");
            }
        }
    }
}
