using KudaGo.Core;
using KudaGo.Core.Events;
using KudaGo.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data;
using KudaGo.Core.News;
using KudaGo.Core.Selections;
using KudaGo.Core.Movies;
using KudaGo.Core.Comments;
using KudaGo.Core.Places;
using KudaGo.Core.Categories;

namespace KudaGo.Client.Model
{
    public interface IDataSource
    {
        Task<IEventListResponse> GetEvents(string next, bool? isFree);
        Task<IEventListResponse> GetEventsWithFilter(string next, string categorySlug, bool? isFree);
        Task<INewsListResponse> GetNews(string next);
        Task<ISelectionListResponse> GetSelections(string next);
        Task<ISelectionDetailsResponse> GetSelectionDetails(long selectionId);
        Task<IMovieListResponse> GetMovies(string next);
        Task<IEventsOfTheDayResponse> GetEventOfTheDay(string next);

        Task<IEventDetailsResponse> GetEventDetails(long eventId);
        Task<INewsDetailsResponse> GetNewsDetails(long newsId);
        Task<IPlaceDetailsResponse> GetPlaceDetails(long placeId);
        Task<ICommentsResponse> GetEventComments(long eventId);
        Task<IMovieDetailsResponse> GetMovieDetails(long movieId);

        Task<IEnumerable<ICategoriesResponse>> GetEventCategories();
        Task<IEnumerable<ICategoriesResponse>> GetPlaceCategories();

        Task<ISearchResponse> Search(string q, string next);
    }

    public class DataSource : IDataSource
    {
        private string _culture = "ru";
        private Location _location = Location.Spb;

        public async Task<IEventListResponse> GetEvents(string next, bool? isFree)
        {
            //var filter = "-concert,-theater,-stock,-education,-kids,-tour,-dance-trainings,-exhibition,-speed-dating";
            var filter = "-concert,-theater,-stock,-kids,-dance-trainings,-speed-dating";
            return await GetEventsWithFilter(next, filter, isFree);
        }

        public async Task<IEventListResponse> GetEventsWithFilter(string next, string categorySlug, bool? isFree)
        {
            var request = new EventListRequest();
            request.Lang = _culture;
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.IsFree = isFree;
            request.Categories = categorySlug;
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandNames.IMAGES, EventListRequest.ExpandNames.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(EventListRequest.FieldNames.DESCRIPTION)
                .WithField(EventListRequest.FieldNames.ID)
                .WithField(EventListRequest.FieldNames.IMAGES)
                .WithField(EventListRequest.FieldNames.PLACE)
                .WithField(EventListRequest.FieldNames.IS_FREE)
                .WithField(EventListRequest.FieldNames.PRICE)
                .WithField(EventListRequest.FieldNames.TITLE)
                .WithField(EventListRequest.FieldNames.DATES)
                .WithField(EventListRequest.FieldNames.CATEGORIES)
                .WithField(EventListRequest.FieldNames.AGE_RESTRICTION).Build();
            request.ActualSince = DateTime.Today;
            request.Location = _location;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<INewsListResponse> GetNews(string next)
        {
            var request = new NewsListRequest();
            request.Lang = _culture;
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Expand = string.Format("{0},{1}", NewsListRequest.ExpandNames.IMAGES, EventListRequest.ExpandNames.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(NewsListRequest.FieldNames.DESCRIPTION)
                .WithField(NewsListRequest.FieldNames.ID)
                .WithField(NewsListRequest.FieldNames.IMAGES)
                .WithField(NewsListRequest.FieldNames.PLACE)
                .WithField(NewsListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(NewsListRequest.FieldNames.TITLE).Build();
            request.Location = _location;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ISelectionListResponse> GetSelections(string next)
        {
            var request = new SelectionListRequest();
            request.Lang = _culture;
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Expand = SelectionListRequest.ExpandNames.IMAGES;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(SelectionListRequest.FieldNames.ID)
                .WithField(SelectionListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(SelectionListRequest.FieldNames.TITLE).Build();
            request.Location = _location;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ISelectionDetailsResponse> GetSelectionDetails(long selectionId)
        {
            var request = new SelectionDetailsRequest();
            request.Lang = _culture;
            request.Expand = SelectionListRequest.ExpandNames.IMAGES;
            request.SelectionId = selectionId;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IMovieListResponse> GetMovies(string next)
        {
            var request = new MovieListRequest();
            request.Lang = _culture;
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Expand = MovieListRequest.ExpandNames.POSTER;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(MovieListRequest.FieldNames.ID)
                .WithField(MovieListRequest.FieldNames.POSTER)
                .WithField(MovieListRequest.FieldNames.IMAGES)
                .WithField(MovieListRequest.FieldNames.AGE_RESTRICTION)
                .WithField(MovieListRequest.FieldNames.YEAR)
                .WithField(MovieListRequest.FieldNames.COUNTRY)
                .WithField(MovieListRequest.FieldNames.RUNNING_TIME)
                .WithField(MovieListRequest.FieldNames.TITLE).Build();
            request.Location = _location;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IEventsOfTheDayResponse> GetEventOfTheDay(string next)
        {
            var request = new EventsOfTheDayRequest();
            request.Lang = _culture;
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(EventsOfTheDayRequest.FieldsNames.DATE)
                .WithField(EventsOfTheDayRequest.FieldsNames.EVENT)
                .WithField(EventsOfTheDayRequest.FieldsNames.LOCATION)
                .Build();
            request.Location = _location;

            var res = await request.ExecuteAsync();
            return res;
        }

        #region Details

        public async Task<IEventDetailsResponse> GetEventDetails(long eventId)
        {
            var request = new EventDetailsRequest();
            request.Lang = _culture;
            request.EventId = eventId;
            request.TextFormat = TextFormatEnum.Text;
            request.Expand = EventListRequest.ExpandNames.PLACE + "," + EventListRequest.ExpandNames.PLACE;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<INewsDetailsResponse> GetNewsDetails(long newsId)
        {
            var request = new NewsDetailsRequest();
            request.Lang = _culture;
            request.NewsId = newsId;
            request.TextFormat = TextFormatEnum.Plain;
            request.Expand = NewsListRequest.ExpandNames.PLACE + "," + NewsListRequest.ExpandNames.PLACE;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IPlaceDetailsResponse> GetPlaceDetails(long placeId)
        {
            var request = new PlaceDetailsRequest();
            request.Lang = _culture;
            request.PlaceId = placeId;
            request.Expand = PlaceListRequest.ExpandNames.IMAGES;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ICommentsResponse> GetEventComments(long eventId)
        {
            var request = new EventCommentsRequest();
            request.Lang = _culture;
            request.EventId = eventId;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IMovieDetailsResponse> GetMovieDetails(long movieId)
        {
            var request = new MovieDetailsRequest();
            request.Lang = _culture;
            request.MovieId = movieId;
            request.Expand = MovieListRequest.ExpandNames.IMAGES;

            var res = await request.ExecuteAsync();
            return res;
        }

        #endregion

        #region Categories

        public async Task<IEnumerable<ICategoriesResponse>> GetEventCategories()
        {
            var request = new EventCategoriesRequest();
            request.Lang = _culture;
            request.OrderBy = CategoryOrderBy.Name;
            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IEnumerable<ICategoriesResponse>> GetPlaceCategories()
        {
            var request = new PlaceCategoriesRequest();
            request.Lang = _culture;
            request.OrderBy = CategoryOrderBy.Name;
            var res = await request.ExecuteAsync();
            return res;
        }

        #endregion

        public async Task<ISearchResponse> Search(string q, string next)
        {
            var request = new SearchRequest();
            request.Next = next;
            request.Q = q;
            request.Lang = _culture;
            request.Location = _location;
            request.Expand = SearchRequest.ExpandFields.PLACES + "," + SearchRequest.ExpandFields.DATES;

            var res = await request.ExecuteAsync();
            return res;
        }
    }
}
