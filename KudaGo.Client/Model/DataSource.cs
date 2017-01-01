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
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandFields.IMAGES, EventListRequest.ExpandFields.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(EventFields.DESCRIPTION)
                .WithField(EventFields.ID)
                .WithField(EventFields.IMAGES)
                .WithField(EventFields.PLACE)
                .WithField(EventFields.IS_FREE)
                .WithField(EventFields.PRICE)
                .WithField(EventFields.TITLE)
                .WithField(EventFields.DATES)
                .WithField(EventFields.CATEGORIES)
                .WithField(EventFields.AGE_RESTRICTION).Build();
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
            request.Expand = string.Format("{0},{1}", NewsListRequest.ExpandNames.IMAGES, EventListRequest.ExpandFields.PLACE);

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
            
            //var fieldBuilder = new FieldsBuilder();
            //request.Fields = fieldBuilder
            //    .WithField(MovieListRequest.FieldNames.ID)
            //    .WithField(MovieListRequest.FieldNames.POSTER)
            //    .WithField(MovieListRequest.FieldNames.IMAGES)
            //    .WithField(MovieListRequest.FieldNames.AGE_RESTRICTION)
            //    .WithField(MovieListRequest.FieldNames.YEAR)
            //    .WithField(MovieListRequest.FieldNames.COUNTRY)
            //    .WithField(MovieListRequest.FieldNames.RUNNING_TIME)
            //    .WithField(MovieListRequest.FieldNames.TITLE).Build();
            //request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }

        #endregion
    }
}
