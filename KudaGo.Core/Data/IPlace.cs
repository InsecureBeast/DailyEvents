using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
{
    public interface IPlace
    {
        long Id { get; }
        string Title { get; }
        string Slug { get; }
        string Address { get; }
        string Phone { get; }
        bool IsStub { get; }
        string SiteUrl { get; }
        ICoordinates Coords { get; }
        string Subway { get; }
        bool IsClosed { get; }
        string Location { get; }
    }

    internal class Place : IPlace
    {
        public Place(JPlace jPlace)
        {
            if (jPlace == null)
            {
                Coords = new Coordinates(new JCoordinates());
                return;
            }

            Id = jPlace.Id;
            Title = jPlace.Title;
            Slug = jPlace.Slug;
            Address = jPlace.Address;
            Phone = jPlace.Phone;
            Subway = jPlace.Subway;
            Location = jPlace.Location;
            IsStub = jPlace.Is_Stub;
            SiteUrl = jPlace.Site_Url;
            Coords = new Coordinates(jPlace.Coords);
            IsClosed = jPlace.Is_Closed;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Subway { get; private set; }
        public string Location { get; private set; }
        public bool IsStub { get; private set; }
        public string SiteUrl { get; private set; }
        public ICoordinates Coords { get; private set; }
        public bool IsClosed { get; private set; }
    }
}
