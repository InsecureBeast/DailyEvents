using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Events.Data
{
    public interface ILocation
    {
        string Slug { get; }
        string Name { get; }
        string TimeZone { get; }
        ICoordinates Coords { get; }
        string Language { get; }
        string Currency { get; }
    }

    public interface ICoordinates
    {
        double Lat { get; }
        double Lon { get; }
    }

    internal class LocationImpl : ILocation
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }

        public Coordinates coords { get; set; }
        public ICoordinates Coords
        {
            get { return coords; }
        }

        public string Language { get; set; }
        public string Currency { get; set; }
    }

    internal class Coordinates : ICoordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
