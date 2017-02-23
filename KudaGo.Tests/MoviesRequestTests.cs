using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core;
using DailyEvents.Core.Movies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyEvents.Core.Search;

namespace UnitTestProject1
{
    [TestClass]
    public class MoviesRequestTests
    {
        [TestMethod]
        public async Task should_get_something()
        {
            var request = new MovieListRequest();
            request.Lang = "ru";
            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder
                .WithField(MovieListRequest.FieldNames.ID)
                .WithField(MovieListRequest.FieldNames.SITE_URL)
                .WithField(MovieListRequest.FieldNames.PUBLICATION_DATE)
                .WithField(MovieListRequest.FieldNames.SLUG)
                .WithField(MovieListRequest.FieldNames.TITLE)
                .WithField(MovieListRequest.FieldNames.DESCRIPTION)
                .WithField(MovieListRequest.FieldNames.BODY_TEXT)
                .WithField(MovieListRequest.FieldNames.IS_EDITORS_CHOICE)
                .WithField(MovieListRequest.FieldNames.FAVORITES_COUNT)
                .WithField(MovieListRequest.FieldNames.GENRES)
                .WithField(MovieListRequest.FieldNames.COMMENTS_COUNT)
                .WithField(MovieListRequest.FieldNames.ORIGINAL_TITLE)
                .WithField(MovieListRequest.FieldNames.LOCALE)
                .WithField(MovieListRequest.FieldNames.COUNTRY)
                .WithField(MovieListRequest.FieldNames.YEAR)
                .WithField(MovieListRequest.FieldNames.LANGUAGE)
                .WithField(MovieListRequest.FieldNames.RUNNING_TIME)
                .WithField(MovieListRequest.FieldNames.BUDGET_CURRENCY)
                //.WithField(MovieListRequest.FieldNames.BUDGET) Пока не работает ((
                .WithField(MovieListRequest.FieldNames.MPAA_RATING)
                .WithField(MovieListRequest.FieldNames.AGE_RESTRICTION)
                .WithField(MovieListRequest.FieldNames.STARS)
                .WithField(MovieListRequest.FieldNames.DIRECTOR)
                .WithField(MovieListRequest.FieldNames.WRITER)
                .WithField(MovieListRequest.FieldNames.AWARDS)
                .WithField(MovieListRequest.FieldNames.TRAILER)
                .WithField(MovieListRequest.FieldNames.IMAGES)
                .WithField(MovieListRequest.FieldNames.POSTER)
                .WithField(MovieListRequest.FieldNames.URL)
                .WithField(MovieListRequest.FieldNames.IMDB_URL)
                .WithField(MovieListRequest.FieldNames.IMDB_RATING).Build();

            request.Expand = MovieListRequest.ExpandNames.IMAGES + "," + MovieListRequest.ExpandNames.POSTER;

            var res = await request.ExecuteAsync();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 0);
            Assert.IsNotNull(res.Next);
            Assert.IsTrue(res.Results.Any());



        }

        [TestMethod]
        public async Task should_get_movie_details()
        {
            var request = new MovieListRequest();
            request.Lang = "ru";

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(MovieListRequest.FieldNames.ID).Build();
            request.Location = Location.Spb;

            //then
            var res = await request.ExecuteAsync();
            var first = res.Results.First();

            var detailsRequest = new MovieDetailsRequest();
            detailsRequest.MovieId = first.Id;
            var actual = await detailsRequest.ExecuteAsync();

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Id, first.Id);
        }
    }
}
