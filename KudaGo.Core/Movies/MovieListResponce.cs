using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JData;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Movies
{
    public interface IMovieListResponse : IResponse
    {
        IEnumerable<IMovieListResult> Results { get; } 
    }

    public interface IMovieListResult
    {
        long Id { get; }
        string SiteUrl { get; }
        DateTime? PublicationDate { get; }
        string Slug { get; }
        string Title { get; }
        string Description { get; }
        string BodyText { get; }
        bool IsEditorsChoice { get; }
        long FavoritesCount { get; }
        IEnumerable<IGenre> Genres { get; }
        long CommentsCount { get; }
        string OriginalTitle { get; }
        string Locale { get; }
        string Country { get; }
        string Year { get; }
        string Language { get; }
        int RunningTime { get; }
        string BudgetCurrency { get; }
        string Budget { get; }
        string MpaaRating { get; }
        string AgeRestriction { get; }
        string Stars { get; }
        string Director { get; }
        string Writer { get; }
        string Awards { get; }
        string Trailer { get; }
        IEnumerable<IImage> Images { get; }
        IImage Poster { get; }
        string Url { get; }
        string ImdbUrl { get; }
        string ImdbRating { get; }
        string Status { get; }
    }

    class MovieListResponse : IMovieListResponse
    {
        public MovieListResponse(JMovieListResponse jResponce)
        {
            if (jResponce == null)
            {
                Results = new IMovieListResult[0];
                return;
            }

            Count = jResponce.Count;
            Next = jResponce.Next;
            Previous = jResponce.Previous;
            Results = jResponce.Results.Select(r => new MovieListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IMovieListResult> Results { get; private set; }
    }

    internal class MovieListResult : IMovieListResult
    {
        public MovieListResult(JMovieListResult jMovieListResult)
        {
            if (jMovieListResult == null)
            {
                Genres = new IGenre[0];
                Images = new IImage[0];
                Poster = new ImageImpl(null);
                return;
            }

            Id = jMovieListResult.Id;
            SiteUrl = jMovieListResult.site_url;
            PublicationDate = DateTimeHelper.GetDateTimeFromUnixTime(jMovieListResult.publication_date);
            Slug = jMovieListResult.Slug;
            Title = jMovieListResult.Title;
            Description = jMovieListResult.Description;
            BodyText = jMovieListResult.body_text;
            IsEditorsChoice = jMovieListResult.is_editors_choice;
            FavoritesCount = jMovieListResult.favorites_count;
            Genres = jMovieListResult.Genres != null
                ? (IEnumerable<IGenre>) jMovieListResult.Genres.Select(g => new Genre(g))
                : new IGenre[0];
            CommentsCount = jMovieListResult.comments_count;
            OriginalTitle = jMovieListResult.original_title;
            Locale = jMovieListResult.Locale;
            Country = jMovieListResult.Country;
            Year = jMovieListResult.Year;
            Language = jMovieListResult.Language;
            RunningTime = jMovieListResult.running_time;
            BudgetCurrency = jMovieListResult.budget_currency;
            Budget = jMovieListResult.Budget;
            MpaaRating = jMovieListResult.mpaa_rating;
            AgeRestriction = jMovieListResult.age_restriction;
            Stars = jMovieListResult.Stars;
            Director = jMovieListResult.Director;
            Writer = jMovieListResult.Writer;
            Trailer = jMovieListResult.Trailer;
            Images = jMovieListResult.Images != null
                ? (IEnumerable<IImage>) jMovieListResult.Images.Select(i => new ImageImpl(i))
                : new IImage[0];

            Poster = new ImageImpl(jMovieListResult.Poster);
            Url = jMovieListResult.Url;
            ImdbUrl = jMovieListResult.imdb_url;
            ImdbRating = jMovieListResult.imdb_rating;
            Status = jMovieListResult.Status;
        }


        public long Id { get; private set; }
        public string SiteUrl { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public string Slug { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string BodyText { get; private set; }
        public bool IsEditorsChoice { get; private set; }
        public long FavoritesCount { get; private set; }
        public IEnumerable<IGenre> Genres { get; private set; }
        public long CommentsCount { get; private set; }
        public string OriginalTitle { get; private set; }
        public string Locale { get; private set; }
        public string Country { get; private set; }
        public string Year { get; private set; }
        public string Language { get; private set; }
        public int RunningTime { get; private set; }
        public string BudgetCurrency { get; private set; }
        public string Budget { get; private set; }
        public string MpaaRating { get; private set; }
        public string AgeRestriction { get; private set; }
        public string Stars { get; private set; }
        public string Director { get; private set; }
        public string Writer { get; private set; }
        public string Awards { get; private set; }
        public string Trailer { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public IImage Poster { get; private set; }
        public string Url { get; private set; }
        public string ImdbUrl { get; private set; }
        public string ImdbRating { get; private set; }
        public string Status { get; private set; }
    }
}
