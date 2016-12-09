using System.Collections.Generic;
using KudaGo.Core.Events.Data;

namespace KudaGo.Core
{
    public interface IResponse
    {
        int Count { get; }
        string Next { get; }
        string Previous { get; }
    }

    public interface IResult
    {
        int FavoritesCount { get; }
        int CommentsCount { get; }
    }

    public interface ISearchResponse : IResponse
    {
        IEnumerable<ISearchResult> Results { get; } 
    }

    public interface ISearchResult : IResult
    {
        int Id { get; }
        string Title { get; }
        string Description { get; }
        CType CType { get; }
        IPlace Place { get; }
        string Address { get; }
        ICoordinates Coords { get; }
        bool Is_Closed { get; }
        bool Is_Stub { get; }
    }

    class SearchResponse : ISearchResponse
    {
        public SearchResponse()
        {
            results = new List<SearchResult>();        
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        
        public IEnumerable<SearchResult> results { get; set; }
        public IEnumerable<ISearchResult> Results
        {
            get { return results; }
        } 
    }

    class SearchResult : Result, ISearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool Is_Closed { get; set; }
        public bool Is_Stub { get; set; }

        public string ctype { get; set; }
        public CType CType
        {
            get { return ctype.GetCType(); }
        }

        public Place place { get; set; }
        public IPlace Place { get { return place; } }

        public Coordinates coords { get; set; }
        public ICoordinates Coords { get { return coords; } }
    }

    class Result : IResult
    {
        public int Favorites_Count { get; set; }
        public int FavoritesCount
        {
            get { return Favorites_Count; }
        }

        public int Comments_Count { get; set; }
        public int CommentsCount
        {
            get { return Comments_Count; }
        }
    }
}
