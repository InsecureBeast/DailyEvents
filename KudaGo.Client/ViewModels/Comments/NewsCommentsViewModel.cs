using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Core.Data;

namespace KudaGo.Client.ViewModels.Comments
{
    class NewsCommentsViewModel : CommentsViewModel
    {
        public NewsCommentsViewModel(long newsId, IDataSource dataSource) : base(newsId, dataSource)
        {
        }

        protected override async Task<IResponse> GetComments(string next)
        {
            IsBusy = true;
            return await _dataSource.GetNewsComments(_id);
        }
    }
}
