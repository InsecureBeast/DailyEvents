using System;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Search
{
    public enum Location
    {
        Spb,
        Moskva,
        Novosibirsk,
        Ekaterinburg,
        NNovgorod,
        Kazan,
        Viborg,
        Samara,
        Krasnodar,
        Sochi,
        Ufa,
        Krasnoyarsk,
        Kiev,
        NewYork
    }

    public enum CType
    {
        News,
        Event,
        Place,
        List,
        Movie
    }
    
    public class SearchRequest : BaseRequest<ISearchResponse>
    {
        public string Expand { get; set; }
        public CType? CType { get; set; }
        public string Q { get; set; }
        public bool? IncludeInactual { get; set; }

        protected override string Build()
        {
            //next search request
            if (!string.IsNullOrEmpty(Next))
                return Next;

            if (Q == null)
                throw new Exception("Q must be set");

            _builder.Append(Q);

            if (CType != null)
                _builder.Append("&ctype=" + CType.Value.GetCType());
            if (Expand != null)
                _builder.Append("&expand=" + Expand);
            if (IncludeInactual != null && IncludeInactual.Value)
                _builder.Append("&include_inactual=1");

            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/search/?q=";
        }

        public override async Task<ISearchResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JSearchResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new SearchResponse(result);
        }

        public string Log()
        {
            return Build();
        }

        public class ExpandFields
        {
            public const string PLACES = "places";
            public const string DATES = "dates";
        }
    }
}
