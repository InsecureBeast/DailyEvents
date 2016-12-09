using System;
using System.Collections.Generic;
using KudaGo.Core.Events.Data;

namespace KudaGo.Core.Events
{
    public interface IEventListResponse : IResponse
    {
        IEnumerable<IEventListResult> Results { get; } 
    }

    public interface IEventListResult : IResult
    {
        string Id { get; }
        DateTime? PublicationDate { get; }
        IEnumerable<IDate> Dates { get; }
        string Title { get; } 
        string Short_Title { get; } 
        string Slug { get; } 
        IPlace Place { get; } 
        string Description { get; } 
        string Body_Text { get; }
        ILocation Location { get; } 
        //string Categories { get; } 
        string Tagline { get; } 
        string Age_Restriction { get; } 
        string Price { get; } 
        string Is_Free { get; } 
        IEnumerable<IImage> Images { get; } 
        string Site_Url { get; } 
        IEnumerable<string> Tags { get; } 
        IEnumerable<IParticipant> Participants { get; } 
    }

    class EventListResponse : IEventListResponse
    {
        public EventListResponse()
        {
            results = new EventListResult[0];    
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        public IEnumerable<EventListResult> results { get; set; }
        public IEnumerable<IEventListResult> Results
        {
            get { return results; }
        }
    }

    internal class EventListResult : Result, IEventListResult
    {
        public EventListResult()
        {
            images = new List<ImageImpl>();
            dates = new DateImpl[0];
            place = new Place();
            location = new LocationImpl();
            participants = new List<Participant>();
        }

        public string Id { get; set; }
        public long publication_date { get; set; }
        public DateTime? PublicationDate
        {
            get
            {
                return DateTimeHelper.GetDateTimeFromUnixTime(publication_date);
            }
        }

        public IEnumerable<DateImpl> dates { get; set; }
        public IEnumerable<IDate> Dates { get { return dates; } }

        public string Title { get; set; }
        public string Short_Title { get; set; }
        public string Slug { get; set; }
        
        public Place place { get; set; }
        public IPlace Place { get { return place; } }
        public string Description { get; set; }
        public string Body_Text { get; set; }

        public LocationImpl location { get; set; }
        public ILocation Location
        {
            get { return location; }
        }

        //public string Categories { get; set; }
        public string Tagline { get; set; }

        public string Age_Restriction { get; set; }
        public string Price { get; set; }
        public string Is_Free { get; set; }
        
        IEnumerable<ImageImpl> images { get; set; }
        public IEnumerable<IImage> Images
        {
            get { return images; }
        }

        public string Site_Url { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<Participant> participants { get; set; }
        public IEnumerable<IParticipant> Participants { get { return participants; } }
    }
}
