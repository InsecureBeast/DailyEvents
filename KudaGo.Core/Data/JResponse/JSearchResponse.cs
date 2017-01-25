using System.Collections.Generic;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    internal class JSearchResponse
    {
        public JSearchResponse()
        {
            Results = new List<JSearchResult>();
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JSearchResult> Results { get; set; }
    }

    internal class JSearchResult : JResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool Is_Closed { get; set; }
        public bool Is_Stub { get; set; }
        public string Ctype { get; set; }
        public JPlace Place { get; set; }
        public JCoordinates Coords { get; set; }
        public JImage first_image { get; set; }
        public string item_url { get; set; }
    }
}
