using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Categories;
using DailyEvents.Core.Events;
using DailyEvents.Core.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CategoriesRequestTests
    {
        [TestMethod]
        public async Task should_get_event_categories()
        {
            var request = new EventCategoriesRequest();
            request.Lang = "ru";
            request.OrderBy = CategoryOrderBy.Name;

            //then
            var res = await request.ExecuteAsync();
            var first = res.First();
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public async Task should_get_place_categories()
        {
            var request = new PlaceCategoriesRequest();
            request.Lang = "ru";
            request.OrderBy = CategoryOrderBy.Name;

            //then
            var res = await request.ExecuteAsync();
            var first = res.First();
            Assert.IsNotNull(first);
        }
    }
}
