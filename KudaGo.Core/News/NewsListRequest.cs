using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;
using DailyEvents.Core.Events;

namespace DailyEvents.Core.News
{
    public class NewsListRequest : BaseRequest<INewsListResponse>
    {
        public override async Task<INewsListResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JNewsListResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new NewsListResponse(result);
        }

        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public string Ids { get; set; }
        public string Tags { get; set; }
        public long? PlaceId { get; set; }
        public bool ActualOnly { get; set; }

        protected override string Build()
        {
            //next search request
            if (!string.IsNullOrEmpty(Next))
                return Next;

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            if (OrederBy != null)
                _builder.Append("&order_by=" + OrederBy.Value.ToString().ToLowerInvariant());

            if (TextFormat != null)
                _builder.Append("&text_format=" + TextFormat.Value.ToString().ToLowerInvariant());

            if (!string.IsNullOrEmpty(Ids))
                _builder.Append("&ids=" + Ids);

            if (!string.IsNullOrEmpty(Tags))
                _builder.Append("&tags=" + Tags);

            if (PlaceId != null)
                _builder.Append("&place_id=" + PlaceId.Value);

            if (ActualOnly)
                _builder.Append("&actual_only=" + ActualOnly);


            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/news/?";
        }

        public enum OrderByEnum
        {
            id,
            publication_date,
            title, 
            slug,
            place,
            description,
            body_text,
            favorites_count,
            comments_count
        }

        public class FieldNames
        {
            public const string ID = "id";// идентификатор
            public const string PUBLICATION_DATE = "publication_date";
            public const string TITLE = "title";// - название
            public const string SLUG = "slug";// - слаг
            public const string PLACE = "place";
            public const string DESCRIPTION = "description";// - описание
            public const string BODY_TEXT = "body_text";// - полное описание
            public const string IMAGES = "images";// - галерея места
            public const string SITE_URL = "site_url";// - адрес места на сайте kudago.com
            public const string FAVORITES_COUNT = "favorites_count";// - число пользователей, добавивших место в избранное
            public const string COMMENTS_COUNT = "comments_count";// - число комментариев
        }

        public class ExpandNames
        {
            public const string IMAGES = "images";
            public const string PLACE = "place";
        }
    }
}
