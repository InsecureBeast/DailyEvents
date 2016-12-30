using System;
using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Selections
{
    public interface ISelectionListResponse : IResponse
    {
        IEnumerable<ISelectionListResult> Results { get; } 
    }

    public interface ISelectionListResult
    {
        long Id { get; }
        DateTime? PublicationDate { get; }
        string Title { get; }
        string Slug { get; }
        string SiteUrl { get; }
        IEnumerable<IImage> Images { get; }
    }

    internal class SelectionListResponse : ISelectionListResponse
    {
        public SelectionListResponse(JSelectionListResponse jResponce)
        {
            if (jResponce == null)
            {
                Results = new ISelectionListResult[0];
                return;
            }

            Count = jResponce.Count;
            Next = jResponce.Next;
            Previous = jResponce.Previous;
            Results = jResponce.Results.Select(r => new SelectionListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<ISelectionListResult> Results { get; private set; }
    }

    internal class SelectionListResult : ISelectionListResult
    {
        public SelectionListResult(JSelectionListResult jResult)
        {
            if (jResult == null)
            {
                Images = new IImage[0];
                return;
            }

            Id = jResult.Id;
            PublicationDate = DateTimeHelper.GetDateTimeFromUnixTime(jResult.Publication_Date);
            Title = jResult.Title;
            Slug = jResult.Slug;
            SiteUrl = jResult.Site_Url;
            Images = jResult.Images != null
                ? (IEnumerable<IImage>)jResult.Images.Select(i => new ImageImpl(i))
                : new IImage[0];
        }

        public long Id { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string SiteUrl { get; private set; }
    }
}
