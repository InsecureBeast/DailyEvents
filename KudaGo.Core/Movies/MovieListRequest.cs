using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Movies
{
    public class MovieListRequest : BaseRequest<IMovieListResponse>
    {
        public override async Task<IMovieListResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JMovieListResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new MovieListResponse(result);
        }

        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public string Ids { get; set; }
        public string Tags { get; set; }
        public string PremieringInLocation { get; set; }
        public long PlaceId { get; set; }
        public DateTime? ActualSince { get; set; }
        public DateTime? ActualUntil { get; set; }

        protected override string GetRelativePath()
        {
            return "/movies/?";
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

            if (ActualSince != null)
                _builder.Append("&actual_since=" + DateTimeHelper.ToUnixTimestamp(ActualSince.Value));

            if (ActualUntil != null)
                _builder.Append("&actual_until=" + DateTimeHelper.ToUnixTimestamp(ActualUntil.Value));

            if (PlaceId != 0)
                _builder.Append("&place_id=" + PlaceId);

            if (!string.IsNullOrEmpty(Tags))
                _builder.Append("&tags=" + Tags);

            if (!string.IsNullOrEmpty(PremieringInLocation))
                _builder.Append("&premiering_in_location=" + PremieringInLocation);

            return base.Build();
        }

        public enum OrderByEnum
        {
            id,
            publication_date,
            slug, 
            title,
            description,
            body_text,
            is_editors_choice,
            favorites_count,
            genres,
            comments_count,
            original_title,
            locale,
            country,
            year,
            language,
            running_time,
            budget_currency,
            budget,
            mpaa_rating,
            age_restriction,
            stars,
            director,
            writer,
            awards,
            trailer,
            url,
            imdb_url,
            imdb_rating,
        }

        public class ExpandNames
        {
            public const string IMAGES = "images";
            public const string POSTER = "poster";
        }

        public class FieldNames
        {
            public const string ID = "id"; //идентификатор
            public const string SITE_URL = "site_url";//  - адрес события на сайте kudago.com
            public const string PUBLICATION_DATE = "publication_date"; // - дата публикации
            public const string SLUG = "slug";// - слаг
            public const string TITLE = "title";//  - название
            public const string DESCRIPTION = "description";//  - описание
            public const string BODY_TEXT = "body_text";//  - полное описание
            public const string IS_EDITORS_CHOICE = "is_editors_choice ";//  - выбор редакции
            public const string FAVORITES_COUNT = "favorites_count";//  - сколько пользователей добавило событие в избранное
            public const string GENRES= "genres";//  - жанр список
            public const string COMMENTS_COUNT = "comments_count";//  - число комментариев 
            public const string ORIGINAL_TITLE = "original_title";//  - оригинальное название
            public const string LOCALE = "locale";//  - язык оригинала
            public const string COUNTRY= "country";//  - страна
            public const string YEAR= "year";//  - год
            public const string LANGUAGE = "language";//  - язык оригинала
            public const string RUNNING_TIME = "running_time";//  - продолжительность
            public const string BUDGET_CURRENCY = "budget_currency";//  - бюджет (валюта)
            public const string BUDGET = "budget";//  - бюджет
            public const string MPAA_RATING = "mpaa_rating";//  - райтинг МРАА
            public const string AGE_RESTRICTION = "age_restriction";//  - возрастное ограничение
            public const string STARS = "stars";//  - актеры
            public const string DIRECTOR = "director";//  - режисер
            public const string WRITER = "writer";//  - сценарист
            public const string AWARDS = "awards";//  - награды
            public const string TRAILER = "trailer";//  - трейлер
            public const string IMAGES = "images";//  - картинки
            public const string POSTER = "poster";//  - постер
            public const string URL = "url";//  - сайт фильма
            public const string IMDB_URL = "imdb_url";//  - сайт фильма
            public const string IMDB_RATING = "imdb_rating";//  - сайт фильма
            public const string STATUS = "status";//  - статус ??
        }
    }
}
