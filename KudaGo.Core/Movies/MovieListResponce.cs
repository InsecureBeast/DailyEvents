using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Movies
{
    public interface IMovieListResponse : IResponse
    {
        IEnumerable<IMovieListResult> Results { get; } 
    }

    public interface IMovieListResult
    {
        long Id { get; }
        string Title { get; }
        IEnumerable<IImage> Poster {get;}
    }

    class MovieListResponse : IMovieListResponse
    {
        public MovieListResponse(JMovieListResponse jResponce)
        {
            if (jResponce == null)
            {
                Results = new IMovieListResult[0];
                return;
            }

            Count = jResponce.Count;
            Next = jResponce.Next;
            Previous = jResponce.Previous;
            Results = jResponce.Results.Select(r => new MovieListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IMovieListResult> Results { get; private set; }
    }

    internal class MovieListResult : IMovieListResult
    {
        public MovieListResult(JMovieListResult jMovieListResult)
        {
            
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<IImage> Poster { get; private set; }
    }
}
