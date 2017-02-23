using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;
using DailyEvents.Core.Events;

namespace DailyEvents.Core.Selections
{
    public class SelectionListRequest : BaseRequest<ISelectionListResponse>
    {
        public override async Task<ISelectionListResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JSelectionListResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new SelectionListResponse(result);
        }

        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public TextFormatEnum? TextFormat { get; set; }
        public string Ids { get; set; }
        public string Tags { get; set; }

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

            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/lists/?";
        }

        public enum OrderByEnum
        {
            id,
            publication_date,
            title, 
            slug
        }

        public class FieldNames
        {
            public const string ID = "id";// идентификатор
            public const string PUBLICATION_DATE = "publication_date";
            public const string TITLE = "title";// - название
            public const string SLUG = "slug";// - слаг
            public const string SITE_URL = "site_url";// - адрес места на сайте kudago.com
        }

        public class ExpandNames
        {
            public const string IMAGES = "images";
        }
    }
}
