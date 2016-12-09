using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Events.Data
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
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Subway { get; set; }
        public string Location { get; set; }

        public bool is_stub { get; set; }
        public bool IsStub { get { return is_stub; } }

        public string site_url { get; set; }
        public string SiteUrl { get { return site_url; } }

        public Coordinates coords { get; set; }
        public ICoordinates Coords { get { return coords; } }
        
        public bool is_closed { get; set; }
        public bool IsClosed { get { return is_closed; } }
    }
}
