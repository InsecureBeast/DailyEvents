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

namespace KudaGo.Client.Model
{
    class DataSource
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
                .WithField(NewsListRequest.FieldNames.TITLE).Build();
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }
    }
}
