using System;
using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JData;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Places
{
    public interface IPlaceDetailsResponse
    {
        string Id { get; }
        string Title { get; }
        string Slug { get; }
        string Address { get; }
        string Timetable { get; }
        string Phone { get; }
        string IsStub { get; }
        string BodyText { get; }
        string Description { get; }
        string SiteUrl { get; }
        string ForeignUrl { get; }
        ICoordinates Coords { get; }
        string Subway { get; }
        long FavoritesCount { get; }
        IEnumerable<IImage> Images { get; }
        long CommentsCount { get; }
        bool IsClosed { get; }
        IEnumerable<string> Categories { get; }
        string ShortTitle { get; }
        IEnumerable<string> Tags { get; }
        string Location { get; }
        string AgeRestriction { get; }
        bool DisableComments { get; }
        bool HasParkingLot { get; }
    }

    class PlaceDetailsResponse : IPlaceDetailsResponse
    {
        public PlaceDetailsResponse(JPlaceDetailsResponse jResult)
        {
            if (jResult == null)
            {
                Coords = new Coordinates(new JCoordinates());
                Images = new IImage[0];
                Categories = new List<string>();
                Tags = new List<string>();
                return;
            }

            Id = jResult.Id;
            Title = jResult.Title;
            Slug = jResult.Slug;
            Address = jResult.Address;
            Timetable = jResult.Timetable;
            Phone = jResult.Phone;
            IsStub = jResult.Is_Stub;
            BodyText = jResult.Body_Text;
            Description = jResult.Description;
            SiteUrl = jResult.Site_Url;
            ForeignUrl = jResult.Foreign_Url;
            Coords = new Coordinates(jResult.Coords);
            Subway = jResult.Subway;
            FavoritesCount = jResult.Favorites_Count;
            Images = jResult.Images.Select(i => new ImageImpl(i));
            CommentsCount = jResult.Comments_Count;
            IsClosed = jResult.Is_closed;
            Categories = jResult.Categories;
            ShortTitle = jResult.Short_Title;
            Tags = jResult.Tags;
            Location = jResult.Location;
            AgeRestriction = jResult.Age_Restriction;
            DisableComments = jResult.Disable_Comments;
            HasParkingLot = jResult.Has_Parking_Lot;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Address { get; private set; }
        public string Timetable { get; private set; }
        public string Phone { get; private set; }
        public string IsStub { get; private set; }
        public string BodyText { get; private set; }
        public string Description { get; private set; }
        public string SiteUrl { get; private set; }
        public string ForeignUrl { get; private set; }
        public ICoordinates Coords { get; private set; }
        public string Subway { get; private set; }
        public long FavoritesCount { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public long CommentsCount { get; private set; }
        public bool IsClosed { get; private set; }
        public IEnumerable<string> Categories { get; private set; }
        public string ShortTitle { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public string Location { get; private set; }
        public string AgeRestriction { get; private set; }
        public bool DisableComments { get; private set; }
        public bool HasParkingLot { get; private set; }
    }
}
