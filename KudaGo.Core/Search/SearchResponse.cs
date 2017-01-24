using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JData;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Search
{
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
        bool IsClosed { get; }
        bool IsStub { get; }
        IImage FirstImage { get; }
    }

    class SearchResponse : ISearchResponse
    {
        public SearchResponse(JSearchResponse response)
        {
            if (response == null)
            {
                Results = new ISearchResult[0];
                return;
            }

            Count = response.Count;
            Next = response.Next;
            Previous = response.Previous;
            Results = response.Results.Select(r => new SearchResult(r));
        }

        public int Count { get;  private set; }
        public string Next { get;  private set; }
        public string Previous { get;  private set; }
        public IEnumerable<ISearchResult> Results { get; private set; }
    } 

    class SearchResult : Result, ISearchResult
    {
        public SearchResult(JSearchResult jResult) : base(jResult)
        {
            if (jResult == null)
            {
                Place = new Place(new JPlace());
                Coords = new Coordinates(new JCoordinates());
                return;
            }

            Id = jResult.Id;
            Title = jResult.Title;
            Description = jResult.Description;
            Address = jResult.Address;
            IsClosed = jResult.Is_Closed;
            IsStub = jResult.Is_Stub;
            CType = jResult.Ctype.GetCType();
            Place = new Place(jResult.Place);
            Coords = new Coordinates(jResult.Coords);
            FirstImage = new ImageImpl(jResult.first_image);
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public bool IsClosed { get; private set; }
        public bool IsStub { get; private set; }
        public CType CType { get; private set; }
        public IPlace Place { get; private set; }
        public ICoordinates Coords { get; private set; }
        public IImage FirstImage { get; private set; }
    }
}
