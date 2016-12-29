using System;
using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JData;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.News
{
    public interface INewsListResponse : IResponse
    {
        IEnumerable<INewsListResult> Results { get; } 
    }

    public interface INewsListResult
    {
        long Id { get; }
        DateTime? PublicationDate { get; }
        string Title { get; }
        string Slug { get; }
        IPlace Place { get; }
        IEnumerable<IImage> Images { get;}
    }

    internal class NewsListResponse : INewsListResponse
    {
        public NewsListResponse(JNewsListResponse jResponce)
        {
            if (jResponce == null)
            {
                Results = new INewsListResult[0];
                return;
            }

            Count = jResponce.Count;
            Next = jResponce.Next;
            Previous = jResponce.Previous;
            Results = jResponce.Results.Select(r => new NewsListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<INewsListResult> Results { get; private set; }
    }

    internal class NewsListResult : INewsListResult
    {
        public NewsListResult(JNewsListResult jNewsListResult)
        {
            if (jNewsListResult == null)
            {
                Place = new Place(new JPlace());
                Images = new IImage[0];
                return;
            }

            Id = jNewsListResult.Id;
            PublicationDate = DateTimeHelper.GetDateTimeFromUnixTime(jNewsListResult.Publication_Date);
            Title = jNewsListResult.Title;
            Slug = jNewsListResult.Slug;
            Place = new Place(jNewsListResult.Place);
            Images = jNewsListResult.Images != null
                ? (IEnumerable<IImage>) jNewsListResult.Images.Select(i => new ImageImpl(i))
                : new IImage[0];
        }

        public long Id { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public IPlace Place { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
    }
}
