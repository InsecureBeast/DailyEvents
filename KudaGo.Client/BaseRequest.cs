using System;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Core
{
    public abstract class BaseRequest<TResponse>
    {
        private string _lang;
        private int _pageSize;
        protected readonly StringBuilder _builder;

        protected BaseRequest()
        {
            _builder = new StringBuilder();
            _builder.Append(ApiService.API_BASE);
            _builder.Append(GetRelativePath());
        }

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

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("PageSize");
                _pageSize = value;
            }
        }

        public int? Page
        {
            get; set;
        }

        public Location? Location
        {
            get; set;
        }

        public bool? IsFree
        {
            get; set;
        }

        public long? Latitude
        {
            get; set;
        }

        public long? Longitude
        {
            get; set;
        }

        public long? Radius
        {
            get; set;
        }

        public string Next
        {
            get; set;
        }

        public abstract Task<TResponse> ExecuteAsync();

        protected virtual string Build()
        {
            if (!string.IsNullOrEmpty(Lang))
                _builder.Append("&lang=" + Lang);
            if (PageSize != 0)
                _builder.Append("&page_size=" + PageSize);
            if (Page != null)
                _builder.Append("&page=" + Page);
            if (Location != null)
                _builder.Append("&location=" + Location.Value.GetLocation());
            if (IsFree != null)
            {
                if (IsFree.Value)
                    _builder.Append("&is_free=true");
                if (!IsFree.Value)
                    _builder.Append("&is_free=1");
            }
            if (Latitude != null)
                _builder.Append("&lat=" + Latitude);
            if (Longitude != null)
                _builder.Append("&lon=" + Longitude);
            if (Radius != null)
                _builder.Append("&=radius" + Radius);

            return _builder.ToString();
        }

        protected virtual string GetRelativePath()
        {
            return string.Empty;
        }
    }
}
