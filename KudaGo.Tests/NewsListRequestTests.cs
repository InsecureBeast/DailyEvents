using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core;
using DailyEvents.Core.Data;
using DailyEvents.Core.News;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class NewsListRequestTests
    {
        [TestMethod]
        public async Task should_get_newslist()
        {
            var fieldsBuilder = new FieldsBuilder();
            var request = new NewsListRequest();
            request.Lang = "ru";
            request.Expand = NewsListRequest.ExpandNames.IMAGES + "," + NewsListRequest.ExpandNames.PLACE;
            request.Fields = fieldsBuilder.WithField(NewsListRequest.FieldNames.TITLE)
                .WithField(NewsListRequest.FieldNames.IMAGES)
                .WithField(NewsListRequest.FieldNames.ID)
                .WithField(NewsListRequest.FieldNames.SLUG)
                .WithField(NewsListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(NewsListRequest.FieldNames.PLACE)
                .Build();

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());

            var first = res.Results.First();
            Assert.IsNotNull(first.Id);
            Assert.IsNotNull(first.Place);
            Assert.IsNotNull(first.Images);
        }

        [TestMethod]
        public async Task should_get_news_details()
        {
            var request = new NewsListRequest();
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var detailsRequest = new NewsDetailsRequest();
            detailsRequest.NewsId = first.Id;

            var detailsResponse = await detailsRequest.ExecuteAsync();
            Assert.IsNotNull(detailsResponse);
        }

        [TestMethod]
        public async Task should_get_news_comments()
        {
            var request = new NewsListRequest();
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var commentsRequest = new NewsCommentsRequest();
            commentsRequest.NewsId = 10942;
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
