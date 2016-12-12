using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using KudaGo.Core.Places;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class PlaceListRequestTests
    {
        [TestMethod]
        public async Task should_get_placelist()
        {
            var request = new PlaceListRequest();
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
        public async Task should_get_place()
        {
            var request = new PlaceListRequest();
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var detailsRequest = new PlaceDetailsRequest();
            detailsRequest.PlaceId = first.Id;

            var detailsResponse = await detailsRequest.ExecuteAsync();
            Assert.IsNotNull(detailsResponse);
        }

        [TestMethod]
        public async Task should_get_place_comments()
        {
            var request = new PlaceListRequest();
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var commentsRequest = new PlaceCommentsRequest();
            commentsRequest.PlaceId = first.Id;
            var fieldBuilder = new FieldsBuilder();
            commentsRequest.Fields = fieldBuilder.WithField(CommentFields.USER)
                .WithField(CommentFields.ID)
                .WithField(CommentFields.IS_DELETED)
                .WithField(CommentFields.DATE_POSTED)
                .WithField(CommentFields.REPLIES_COUNT)
                .WithField(CommentFields.REPLY_TO)
                .WithField(CommentFields.TEXT)
                .WithField(CommentFields.THREAD)
                .Build();

            var commentsResponse = await commentsRequest.ExecuteAsync();
            Assert.IsNotNull(commentsResponse);
        }
    }
}
