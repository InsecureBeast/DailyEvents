using System;
using System.Linq;
using System.Threading.Tasks;
using KudaGo.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class SearchRequestTests
    {
        [TestMethod]
        public void should_build_request()
        {
            var request = new SearchRequest();
            request.Lang = "ru";
            request.Expand = SearchRequest.ExpandFields.DATES;
            request.IncludeInactual = false;
            request.IsFree = true;
            request.CType = CType.News;
            request.Q = "";

            var log = request.Log();
            Assert.AreEqual("https://kudago.com/public-api/v1.3/search/?q=&ctype=news&expand=dates&lang=ru&is_free=true", log);
        }

        [TestMethod]
        public void should_not_accept_wrong_language()
        {
            var request = new SearchRequest();
            //Assert.ThrowsException<NotSupportedException>(() => request.Lang = "es");
        }

        [TestMethod]
        public async Task should_search_something()
        {
            var request = new SearchRequest();
            request.Lang = "ru";
            request.Expand = SearchRequest.ExpandFields.DATES;
            request.CType = CType.News;
            request.Q = "новости";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public async Task should_execute_next()
        {
            var request = new SearchRequest();
            request.Lang = "ru";
            request.Expand = SearchRequest.ExpandFields.DATES;
            request.CType = CType.News;
            request.Q = "новости";

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);

            var nextRequest = new SearchRequest();
            nextRequest.Next = res.Next;
            var nextRes = await nextRequest.ExecuteAsync();
            Assert.IsNotNull(nextRes);
        }
    }
}
