using System;
using System.Collections.Generic;
using DailyEvents.Core.Data.JData;

namespace DailyEvents.Core.Data.JResponse
{
    class JPlaceDetailsResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Timetable { get; set; }
        public string Phone { get; set; }
        public string Is_Stub { get; set; }
        public string Body_Text { get; set; }
        public string Description { get; set; }
        public string Site_Url { get; set; }
        public string Foreign_Url { get; set; }
        public JCoordinates Coords { get; set; }
        public string Subway { get; set; }
        public long Favorites_Count { get; set; }
        public IEnumerable<JImage> Images { get; set; }
        public long Comments_Count { get; set; }
        public bool Is_closed { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string Short_Title { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Location { get; set; }
        public string Age_Restriction { get; set; }
        public bool Disable_Comments { get; set; }
        public bool Has_Parking_Lot { get; set; }
    }
}
