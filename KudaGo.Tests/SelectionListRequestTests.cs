using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core;
using DailyEvents.Core.Data;
using DailyEvents.Core.News;
using DailyEvents.Core.Selections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class SelectionListRequestTests
    {
        [TestMethod]
        public async Task should_get_selectionlist()
        {
            var fieldsBuilder = new FieldsBuilder();
            var request = new SelectionListRequest();
            request.Lang = "ru";
            request.Expand = NewsListRequest.ExpandNames.IMAGES + "," + NewsListRequest.ExpandNames.PLACE;
            request.Fields = fieldsBuilder
                .WithField(SelectionListRequest.FieldNames.TITLE)
                .WithField(SelectionListRequest.FieldNames.ID)
                .WithField(SelectionListRequest.FieldNames.SLUG)
                .WithField(SelectionListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(SelectionListRequest.FieldNames.SITE_URL)
                .Build();

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());
        }

        [TestMethod]
        public async Task should_get_selection_details()
        {
            var request = new SelectionListRequest();;
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var detailsRequest = new SelectionDetailsRequest();
            detailsRequest.SelectionId= first.Id;

            var detailsResponse = await detailsRequest.ExecuteAsync();
            Assert.IsNotNull(detailsResponse);
        }

        [TestMethod]
        public async Task should_get_selection_comments()
        {
            var request = new SelectionListRequest();
            request.Lang = "ru";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);

            var first = res.Results.First();
            var commentsRequest = new SelectionCommentsRequest();
            commentsRequest.SelectionId = 4103;
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
