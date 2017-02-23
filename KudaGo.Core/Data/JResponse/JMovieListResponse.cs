using System;
using System.Collections.Generic;
using DailyEvents.Core.Data.JData;

namespace DailyEvents.Core.Data.JResponse
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
        public long publication_date { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string body_text { get; set; }
        public bool is_editors_choice { get; set; }
        public long favorites_count { get; set; }
        public IEnumerable<JGenre> Genres { get; set; }
        public long comments_count { get; set; }
        public string original_title { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Language { get; set; }
        public int running_time { get; set; }
        public string budget_currency { get; set; }
        public string Budget { get; set; }
        public string mpaa_rating { get; set; }
        public string age_restriction { get; set; }
        public string Stars { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Awards { get; set; }
        public string Trailer { get; set; }
        public IEnumerable<JImage> Images { get; set; }
        public JImage Poster { get; set; }
        public string Url { get; set; }
        public string imdb_url { get; set; }
        public string imdb_rating { get; set; }
        public string Status { get; set; }
    }
}
