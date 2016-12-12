using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
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
        public LocationImpl(JLocation jLocation)
        {
            if (jLocation == null)
            {
                Coords = new Coordinates(new JCoordinates());
                return;
            }

            Slug = jLocation.Slug;
            Name = jLocation.Name;
            TimeZone = jLocation.TimeZone;
            Coords = new Coordinates(jLocation.Coords);
            Language = jLocation.Language;
            Currency = jLocation.Currency;
        }

        public string Slug { get; private set; }
        public string Name { get; private set; }
        public string TimeZone { get; private set; }
        public ICoordinates Coords { get; private set; }
        public string Language { get; private set; }
        public string Currency { get; private set; }
    }

    internal class Coordinates : ICoordinates
    {
        public Coordinates(JCoordinates coords)
        {
            if (coords == null)
                return;

            Lat = coords.Lat;
            Lon = coords.Lon;
        }

        public double Lat { get; private set; }
        public double Lon { get; private set; }
    }
}
