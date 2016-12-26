using System;
using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JData;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Events
{
    public interface IEventListResponse : IResponse
    {
        IEnumerable<IEventListResult> Results { get; } 
    }

    public interface IEventListResult : IResult
    {
        long Id { get; }
        DateTime? PublicationDate { get; }
        IEnumerable<IDate> Dates { get; }
        string Title { get; } 
        string ShortTitle { get; } 
        string Slug { get; } 
        IPlace Place { get; } 
        string Description { get; } 
        string BodyText { get; }
        ILocation Location { get; } 
        IEnumerable<string> Categories { get; } 
        string Tagline { get; } 
        string AgeRestriction { get; } 
        string Price { get; } 
        string IsFree { get; } 
        IEnumerable<IImage> Images { get; } 
        string SiteUrl { get; } 
        IEnumerable<string> Tags { get; } 
        IEnumerable<IParticipant> Participants { get; } 
    }

    class EventListResponse : IEventListResponse
    {
        public EventListResponse(JEventListResponse jResponse)
        {
            if (jResponse == null)
            {
                Results = new IEventListResult[0];
                return;
            }

            Count = jResponse.Count;
            Next = jResponse.Next;
            Previous = jResponse.Previous;
            Results = jResponse.Results.Select(r => new EventListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IEventListResult> Results { get; private set; }
        
    }

    internal class EventListResult : Result, IEventListResult
    {
        public EventListResult(JEventListResult jResult) : base(jResult)
        {
            if (jResult == null)
            {
                Images = new List<ImageImpl>();
                Dates = new DateImpl[0];
                Place = new Place(new JPlace());
                Location = new LocationImpl(new JLocation());
                Participants = new List<Participant>();
                Categories = new string[0];
                return;
            }
            Id = jResult.Id;
            PublicationDate = DateTimeHelper.GetDateTimeFromUnixTime(jResult.Publication_Date);
            Dates = jResult.Dates.Select(d => new DateImpl(d));
            Title = jResult.Title;
            ShortTitle = jResult.Short_Title;
            Slug = jResult.Slug;
            Place = new Place(jResult.Place);
            Description = jResult.Description;
            BodyText = jResult.Body_Text;
            Location = new LocationImpl(jResult.Location);
            Categories = jResult.Categories;
            Tagline = jResult.Tagline;
            AgeRestriction = jResult.Age_Restriction == "0" ? "0+" : jResult.Age_Restriction;
            Price = jResult.Price;
            IsFree = jResult.Is_Free;
            Images = jResult.Images.Select(i => new ImageImpl(i));
            SiteUrl = jResult.Site_Url;
            Tags = jResult.Tags;
            Participants = jResult.Participants.Select(p => new Participant(p));
        }

        public long Id { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public IEnumerable<IDate> Dates { get; private set; }
        public string Title { get; private set; }
        public string ShortTitle { get; private set; }
        public string Slug { get; private set; }
        public IPlace Place { get; private set; }
        public string Description { get; private set; }
        public string BodyText { get; private set; }
        public ILocation Location { get; private set; }
        public IEnumerable<string> Categories { get; private set; }
        public string Tagline { get; private set; }
        public string AgeRestriction { get; private set; }
        public string Price { get; private set; }
        public string IsFree { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public string SiteUrl { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public IEnumerable<IParticipant> Participants { get; private set; }
    }
}
