using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core;
using KudaGo.Core.Events;
using KudaGo.Core.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class EventListRequestTests
    {
        [TestMethod]
        public async Task should_get_something()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            
            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());

            var first = res.Results.First();
            Assert.IsNotNull(first.Id);
            Assert.IsNotNull(first.Title);
            Assert.IsNotNull(first.Slug);
        }

        [TestMethod]
        public async Task should_get_events_with_fields()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Fields = "age_restriction,is_free";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());

            var first = res.Results.First();
            Assert.IsNotNull(first.AgeRestriction);
            Assert.IsNotNull(first.IsFree);
        }

        [TestMethod]
        public async Task should_get_events_with_fields_2()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1},{2},{3},{4}", 
                EventListRequest.ExpandNames.IMAGES, 
                EventListRequest.ExpandNames.PLACE, 
                EventListRequest.ExpandNames.LOCATION, 
                EventListRequest.ExpandNames.DATES, 
                EventListRequest.ExpandNames.PARTICIPANTS);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(EventListRequest.FieldNames.BODY_TEXT)
                .WithField(EventListRequest.FieldNames.COMMENTS_COUNT)
                .WithField(EventListRequest.FieldNames.CATEGORIES)
                .WithField(EventListRequest.FieldNames.DESCRIPTION)
                .WithField(EventListRequest.FieldNames.DATES)
                .WithField(EventListRequest.FieldNames.FAVORITES_COUNT)
                .WithField(EventListRequest.FieldNames.AGE_RESTRICTION)
                .WithField(EventListRequest.FieldNames.ID)
                .WithField(EventListRequest.FieldNames.IMAGES)
                .WithField(EventListRequest.FieldNames.IS_FREE)
                .WithField(EventListRequest.FieldNames.BODY_TEXT)
                .WithField(EventListRequest.FieldNames.LOCATION)
                .WithField(EventListRequest.FieldNames.PARTICIPANTS)
                .WithField(EventListRequest.FieldNames.PLACE)
                .WithField(EventListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(EventListRequest.FieldNames.PRICE)
                .WithField(EventListRequest.FieldNames.SHORT_TITLE)
                .WithField(EventListRequest.FieldNames.SITE_URL)
                .WithField(EventListRequest.FieldNames.SLUG).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());

            var first = res.Results.First();
            Assert.IsNotNull(first.BodyText);
            Assert.IsNotNull(first.CommentsCount);
        }

        [TestMethod]
        public async Task should_get_event_details()
        {
            var request = new EventListRequest();
            request.Lang = "ru";

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(EventListRequest.FieldNames.ID).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            //then
            var res = await request.ExecuteAsync();
            var first = res.Results.First();

            var detailsRequest = new EventDetailsRequest();
            detailsRequest.EventId = first.Id;
            var actual = await detailsRequest.ExecuteAsync();

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Id, first.Id);
        }

        [TestMethod]
        public async Task should_get_event_comments()
        {
            var request = new EventListRequest();
            request.Lang = "ru";

            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            //then
            var res = await request.ExecuteAsync();
            var first = res.Results.First();

            var detailsRequest = new EventCommentsRequest();
            detailsRequest.EventId = first.Id;
            var actual = await detailsRequest.ExecuteAsync();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public async Task should_get_events_of_the_day()
        {
            var request = new EventsOfTheDayRequest();
            request.Lang = "ru";
            request.Location = Location.Spb;

            //then
            var res = await request.ExecuteAsync();
            var first = res.Results.First();
            Assert.IsNotNull(first);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Не найдено")]
        public async Task should_throw_exception()
        {
            var detailsRequest = new EventDetailsRequest();
            detailsRequest.Lang = "ru";
            detailsRequest.EventId = 10000000000000000;
            var actual = await detailsRequest.ExecuteAsync();
        }

        [TestMethod]
        public async Task should_get_event_kids()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Categories = "kids";
            request.Location = Location.Spb;

            //then
            var res = await request.ExecuteAsync();
            var first = res.Results.First();

            var detailsRequest = new EventCommentsRequest();
            detailsRequest.EventId = first.Id;
            var actual = await detailsRequest.ExecuteAsync();

            Assert.IsNotNull(actual);
        }
    }
}
