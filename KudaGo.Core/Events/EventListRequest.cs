using System;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Events
{
    public class EventListRequest : BaseRequest<IEventListResponse>
    {
        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public string Ids { get; set; }
        public DateTime? ActualSince { get; set; }
        public DateTime? ActualUntil { get; set; }
        public long? PlaceId { get; set; }
        public long? ParentId { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }

        public override async Task<IEventListResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JEventListResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new EventListResponse(result);
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

            if (!string.IsNullOrEmpty(Ids))
                _builder.Append("&ids=" + Ids);

            if (ActualSince != null)
                _builder.Append("&actual_since=" + DateTimeHelper.ToUnixTimestamp(ActualSince.Value));

            if (ActualUntil != null)
                _builder.Append("&actual_until=" + DateTimeHelper.ToUnixTimestamp(ActualUntil.Value));

            if (PlaceId != null)
                _builder.Append("&place_id=" + PlaceId.Value);

            if (ParentId != null)
                _builder.Append("&parent_id=" + ParentId.Value);

            if (!string.IsNullOrEmpty(Categories))
                _builder.Append("&categories=" + Categories);

            if (!string.IsNullOrEmpty(Tags))
                _builder.Append("&tags=" + Tags);

            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/events/?";
        }

        public class ExpandNames
        {
            public const string IMAGES = "images";
            public const string PLACE = "place";
            public const string LOCATION = "location";
            public const string DATES = "dates";
            public const string PARTICIPANTS = "participants";
        }

        public enum OrderByEnum
        {
            Id,
            Publication_date,
            Title, 
            Slug,
            Place, 
            Description, 
            Body_text, 
            Location, 
            Tagline, 
            Age_restriction,
            Price, 
            Is_free,
            Favorites_count,
            Comments_count,
            Short_title
        }

        public class FieldNames
        {
            public const string ID = "id"; //идентификатор
            public const string PUBLICATION_DATE = "publication_date"; // - дата публикации
            public const string DATES = "dates";// - даты проведения
            public const string TITLE = "title";//  - название
            public const string SHORT_TITLE = "short_title";//  - короткое название
            public const string SLUG = "slug";// - слаг
            public const string PLACE = "place";//  - место проведения
            public const string DESCRIPTION = "description";//  - описание
            public const string BODY_TEXT = "body_text";//  - полное описание
            public const string LOCATION = "location";//  - город проведения
            public const string CATEGORIES = "categories";//  - список категорий
            public const string TAGLINE = "tagline";//  - тэглайн
            public const string AGE_RESTRICTION = "age_restriction";//  - возрастное ограничение
            public const string PRICE = "price";//  - стоимость
            public const string IS_FREE = "is_free";//  - бесплатное ли событие
            public const string IMAGES = "images";//  - картинки
            public const string FAVORITES_COUNT = "favorites_count";//  - сколько пользователей добавило событие в избранное
            public const string COMMENTS_COUNT = "comments_count";//  - число комментариев к событию
            public const string SITE_URL = "site_url";//  - адрес события на сайте kudago.com
            public const string TAGS = "tags";//  - тэги события
            public const string PARTICIPANTS = "participants";//  - агенты события
        }
    }
}
