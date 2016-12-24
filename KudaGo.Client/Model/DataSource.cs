using KudaGo.Core;
using KudaGo.Core.Events;
using KudaGo.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Model
{
    class DataSource
    {
        public async Task<IEventListResponse> GetEvents()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.TextFormat = TextFormatEnum.Plain;
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandFields.IMAGES, EventListRequest.ExpandFields.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(EventFields.BODY_TEXT)
                .WithField(EventFields.DESCRIPTION)
                .WithField(EventFields.ID)
                .WithField(EventFields.IMAGES)
                .WithField(EventFields.PLACE)
                .WithField(EventFields.PRICE)
                .WithField(EventFields.TITLE)
                .WithField(EventFields.DATES)
                .WithField(EventFields.AGE_RESTRICTION).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            return res;
        }
    }
}
