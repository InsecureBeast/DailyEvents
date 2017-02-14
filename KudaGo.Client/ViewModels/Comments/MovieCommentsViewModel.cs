using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Core.Data;

namespace KudaGo.Client.ViewModels.Comments
{
    class MovieCommentsViewModel : CommentsViewModel
    {
        public MovieCommentsViewModel(long movieId, IDataSource dataSource) : base(movieId, dataSource)
        {
        }

        protected override async Task<IResponse> GetComments(string next)
        {
            IsBusy = true;
            return await _dataSource.GetMovieComments(_id);
        }
    }
}
