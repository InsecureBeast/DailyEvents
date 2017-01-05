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

namespace KudaGo.Client.Model
{
    public class DataSource
    {
        public async Task<IEventListResponse> GetEvents(string next)
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Categories = "-concert,-theater";
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
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<INewsListResponse> GetNews(string next)
        {
            var request = new NewsListRequest();
            request.Lang = "ru";
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
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ISelectionListResponse> GetSelections(string next)
        {
            var request = new SelectionListRequest();
            request.Lang = "ru";
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Expand = SelectionListRequest.ExpandNames.IMAGES;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(SelectionListRequest.FieldNames.ID)
                .WithField(SelectionListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(SelectionListRequest.FieldNames.TITLE).Build();
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ISelectionDetailsResponse> GetSelectionDetails(long selectionId)
        {
            var request = new SelectionDetailsRequest();
            request.Lang = "ru";
            request.Expand = SelectionListRequest.ExpandNames.IMAGES;
            request.SelectionId = selectionId;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IMovieListResponse> GetMovies(string next)
        {
            var request = new MovieListRequest();
            request.Lang = "ru";
            request.TextFormat = TextFormatEnum.Plain;
            request.Next = next;
            request.Expand = MovieListRequest.ExpandFields.POSTER;

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
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }

        #region Details

        public async Task<IEventDetailsResponse> GetEventDetails(long eventId)
        {
            var request = new EventDetailsRequest();
            request.Lang = "ru";
            request.EventId = eventId;
            request.TextFormat = TextFormatEnum.Text;
            request.Expand = EventListRequest.ExpandNames.PLACE + "," + EventListRequest.ExpandNames.PLACE;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<INewsDetailsResponse> GetNewsDetails(long newsId)
        {
            var request = new NewsDetailsRequest();
            request.Lang = "ru";
            request.NewsId = newsId;
            request.TextFormat = TextFormatEnum.Plain;
            request.Expand = NewsListRequest.ExpandNames.PLACE + "," + NewsListRequest.ExpandNames.PLACE;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IPlaceDetailsResponse> GetPlaceDetails(long placeId)
        {
            var request = new PlaceDetailsRequest();
            request.Lang = "ru";
            request.PlaceId = placeId;
            request.Expand = PlaceListRequest.ExpandNames.IMAGES;
            request.Fields = PlaceListRequest.FieldNames.IMAGES;

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<ICommentsResponse> GetEventComments(long eventId)
        {
            var request = new EventCommentsRequest();
            request.Lang = "ru";
            request.EventId = eventId;

            var res = await request.ExecuteAsync();
            return res;
        }

        #endregion

        #region Images
        public async Task<IPlaceListResponse> GetPlaceImages(string ids)
        {
            var request = new PlaceListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1}", PlaceListRequest.ExpandNames.IMAGES);
            request.Ids = ids;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(PlaceListRequest.FieldNames.IMAGES)
                .Build();

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<IEventListResponse> GetEventImages(string ids)
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandNames.IMAGES);
            request.Ids = ids;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(EventListRequest.FieldNames.IMAGES)
                .Build();

            var res = await request.ExecuteAsync();
            return res;
        }

        public async Task<INewsListResponse> GetNewsImages(string ids)
        {
            var request = new NewsListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1}", NewsListRequest.ExpandNames.IMAGES);
            request.Ids = ids;

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(NewsListRequest.FieldNames.IMAGES)
                .Build();

            var res = await request.ExecuteAsync();
            return res;
        }

        #endregion
    }
}
