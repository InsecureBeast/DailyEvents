using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Core.Data.JResponse
{
    class JMovieListResponse
    {
        public JMovieListResponse()
        {
            Results = new JMovieListResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JMovieListResult> Results { get; set; }
    }

    class JMovieListResult
    {
        public long Id { get; set; }
        public string site_url { get; set; }
        public string publication_date { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string body_text { get; set; }
        public string is_editors_choice { get; set; }
        public string favorites_count { get; set; }
        public string Genres { get; set; }
        public string comments_count { get; set; }
        public string original_title { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Language { get; set; }
        public string running_time { get; set; }
        public string budget_currency { get; set; }
        public string Budget { get; set; }
        public string mpaa_rating { get; set; }
        public string age_restriction { get; set; }
        public string Stars { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Awards { get; set; }
        public string Trailer { get; set; }
        public string Images { get; set; }
        public string Poster { get; set; }
        public string Url { get; set; }
        public string imdb_url { get; set; }
        public string imdb_rating { get; set; }
        public string Status { get; set; }
    }
}
