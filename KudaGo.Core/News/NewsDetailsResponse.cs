using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.News
{
    public interface INewsDetailsResponse : INewsListResult
    {
        string BodyText { get; }
        string SiteUrl { get; }
        long FavoritesCount { get; }
        long CommentsCount { get; }
    }

    class NewsDetailsResponse : NewsListResult, INewsDetailsResponse
    {
        public NewsDetailsResponse(JNewsDetailsResponse jResponce): base(jResponce)
        {
            Description = jResponce.Description;
            BodyText = jResponce.Body_Text;
            SiteUrl = jResponce.Site_Url;
            FavoritesCount = jResponce.Favorites_Count;
            CommentsCount = jResponce.Comments_count;
        }

        public string BodyText { get; private set; }
        public string SiteUrl { get; private set; }
        public long FavoritesCount { get; private set; }
        public long CommentsCount { get; private set; }
    }
}
