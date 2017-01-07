using KudaGo.Core.Data.JResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Core.Movies
{
    public interface IMovieDetailsResponse : IMovieListResult
    {
        //bool DisableComments { get; }

    }

    public class MovieDetailsRequest : BaseRequest<IMovieDetailsResponse>
    {
        public long MovieId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }
        //public TextFormatEnum? TextFormat { get; set; }

        public override async Task<IMovieDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JMovieDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new MovieDetailsResponse(result);
        }

        protected override string GetRelativePath()
        {
            return "/movies/";
        }

        protected override string Build()
        {
            if (MovieId <= 0)
                throw new Exception("MovieId must be set");

            _builder.Append(MovieId + "/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            //if (TextFormat != null)
            //    _builder.Append("&text_format=" + TextFormat.Value.ToString().ToLowerInvariant());

            return base.Build();
        }
    }

    internal class MovieDetailsResponse : MovieListResult, IMovieDetailsResponse
    {
        public MovieDetailsResponse(JMovieDetailsResponse jResult) : base(jResult)
        {
        }
    }
}
