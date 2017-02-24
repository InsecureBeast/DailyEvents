using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JResponse;
using DailyEvents.Core.Events;

namespace DailyEvents.Core.Places
{
    public class PlaceListRequest : BaseRequest<IPlaceListResponse>
    {
        public override async Task<IPlaceListResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JPlaceListResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new PlaceListResponse(result);
        }

        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public string Ids { get; set; }
        public string HasShowings { get; set; }
        public DateTime? ShowingSince { get; set; }
        public DateTime? ShowingUntil { get; set; }
        public long? ParentId { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }

        protected override string GetRelativePath()
        {
            return "/places/?";
        }

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

            if (!string.IsNullOrEmpty(HasShowings))
                _builder.Append("&has_showings=" + HasShowings);

            if (ShowingSince != null)
                _builder.Append("&showing_since=" + DateTimeHelper.ToUnixTimestamp(ShowingSince.Value));

            if (ShowingUntil != null)
                _builder.Append("&showing_until=" + DateTimeHelper.ToUnixTimestamp(ShowingUntil.Value));

            if (ParentId != null)
                _builder.Append("&parent_id=" + ParentId.Value);

            if (!string.IsNullOrEmpty(Categories))
                _builder.Append("&categories=" + Categories);

            if (!string.IsNullOrEmpty(Tags))
                _builder.Append("&tags=" + Tags);

            return base.Build();
        }

        public class FieldNames
        {
            public const string ID = "id";// идентификатор
            public const string TITLE = "title";// - название
            public const string SHORT_TITLE = "short_title";// - короткое название
            public const string SLUG = "slug";// - слаг
            public const string ADDRESS = "address";// - адрес
            public const string LOCATION = "location";// - город
            public const string TIMETABLE = "timetable";// - расписание
            public const string PHONE = "phone";// - телефон
            public const string IS_STUB = "is_stub";// - является ли заглушкой
            public const string IMAGES = "images";// - галерея места
            public const string DESCRIPTION = "description";// - описание
            public const string BODY_TEXT = "body_text";// - полное описание
            public const string SITE_URL = "site_url";// - адрес места на сайте kudago.com
            public const string FOREIGN_URL = "foreign_url";// - сайт места
            public const string COORDS = "coords"; //- координаты места
            public const string SUBWAY = "subway";// - метро рядом
            public const string FAVORITES_COUNT = "favorites_count";// - число пользователей, добавивших место в избранное
            public const string COMMENTS_COUNT = "comments_count";// - число комментариев
            public const string IS_CLOSED = "is_closed";// - закрыто ли место
            public const string CATEGORIES = "categories";// - список категорий
            public const string TAGS = "tags";// - тэги места
        }

        public class ExpandNames
        {
            public const string IMAGES = "images";
        }

        public enum OrderByEnum
        {
            Id,
            Title,
            Slug,
            Address,
            Phone,
            Is_Stub,
            Body_Text,
            Description,
            Foreign_Url,
            Subway,
            Favorites_Count,
            Comments_Count,
            Is_Closed,
            Short_Title,
            Location
        }

        public class HasShowingsNames
        {
            public const string MOVIE = "movie";
        }
    }
}
