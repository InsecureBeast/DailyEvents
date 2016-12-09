using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core;
using KudaGo.Core.Events;
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
            Assert.IsNotNull(first.Age_Restriction);
            Assert.IsNotNull(first.Is_Free);
        }

        [TestMethod]
        public async Task should_get_events_with_fields_2()
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1},{2},{3},{4}", 
                EventListRequest.ExpandFields.IMAGES, 
                EventListRequest.ExpandFields.PLACE, 
                EventListRequest.ExpandFields.LOCATION, 
                EventListRequest.ExpandFields.DATES, 
                EventListRequest.ExpandFields.PARTICIPANTS);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(FieldsBuilder.BODY_TEXT)
                .WithField(FieldsBuilder.COMMENTS_COUNT)
                .WithField(FieldsBuilder.CATEGORIES)
                .WithField(FieldsBuilder.DESCRIPTION)
                .WithField(FieldsBuilder.DATES)
                .WithField(FieldsBuilder.FAVORITES_COUNT)
                .WithField(FieldsBuilder.AGE_RESTRICTION)
                .WithField(FieldsBuilder.ID)
                .WithField(FieldsBuilder.IMAGES)
                .WithField(FieldsBuilder.IS_FREE)
                .WithField(FieldsBuilder.BODY_TEXT)
                .WithField(FieldsBuilder.LOCATION)
                .WithField(FieldsBuilder.PARTICIPANTS)
                .WithField(FieldsBuilder.PLACE)
                .WithField(FieldsBuilder.PUBLICATION_DATE)
                .WithField(FieldsBuilder.PRICE)
                .WithField(FieldsBuilder.SHORT_TITLE)
                .WithField(FieldsBuilder.SITE_URL)
                .WithField(FieldsBuilder.SLUG).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());

            var first = res.Results.First();
            Assert.IsNotNull(first.Body_Text);
            Assert.IsNotNull(first.CommentsCount);
        }

        [TestMethod]
        public async Task should_get_event_details()
        {
            var request = new EventListRequest();
            request.Lang = "ru";

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(FieldsBuilder.ID).Build();
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

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(FieldsBuilder.ID).Build();
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
    }
}
