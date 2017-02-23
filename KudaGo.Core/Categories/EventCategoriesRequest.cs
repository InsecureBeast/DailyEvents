using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Categories
{
    public class EventCategoriesRequest
    {
        private readonly StringBuilder _builder;
        private string _lang;

        public EventCategoriesRequest()
        {
            _builder = new StringBuilder();
            _builder.Append(ApiService.API_BASE);
            _builder.Append(GetRelativePath());
        }

        public string Fields { get; set; }
        public CategoryOrderBy? OrderBy { get; set; }

        public string Lang
        {
            get { return _lang; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (!value.Equals("ru") && !value.Equals("en"))
                    throw new NotSupportedException(string.Format("not supported language {0}", value));
                _lang = value;
            }
        }

        public async Task<IEnumerable<ICategoriesResponse>> ExecuteAsync()
        {
            var request = new ClientServiceRequest<IEnumerable<JCategoriesResponse>>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            if (result == null)
                return new ICategoriesResponse[0];

            return result.Select(r => new CategoriesResponse(r));
        }

        protected virtual string GetRelativePath()
        {
            return "/event-categories/?";
        }

        private string Build()
        {
            if (!string.IsNullOrEmpty(Lang))
                _builder.Append("&lang=" + Lang);
            
            if (Fields != null)
                _builder.Append("&fields=" + Fields);

            if (OrderBy != null)
                _builder.Append("&=order_by=" + OrderBy.Value.ToString().ToLowerInvariant());

            return _builder.ToString();
        }
    }

    public enum CategoryOrderBy
    {
        Slug,
        Name
    }

    public class CategoryFieldNames
    {
        public const string SLUG = "slug"; // слаг
        public const string NAME = "name"; // - название
    }
}
