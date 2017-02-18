using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
{
    public interface IResponse
    {
        int Count { get; }
        string Next { get; }
        string Previous { get; }
    }

    public interface IResult
    {
        int FavoritesCount { get; }
        int CommentsCount { get; }
    }

    internal class Result : IResult
    {
        public Result(JResult jResult)
        {
            if (jResult == null)
                return;

            FavoritesCount = jResult.Favorites_Count;
            CommentsCount = jResult.Comments_Count;
        }

        public int FavoritesCount { get; private set; }
        public int CommentsCount { get; private set; }
    }

    public class EmptyResponce : IResponse
    {
        public int Count
        {
            get { return 0; }
        }

        public string Next
        {
            get { return string.Empty; }
        }

        public string Previous
        {
            get { return string.Empty; }
        }
    }
}
