using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Core.Data;

namespace KudaGo.Client.ViewModels.Comments
{
    class SelectionCommentsViewModel : CommentsViewModel
    {
        public SelectionCommentsViewModel(long selectionId, IDataSource dataSource) : base(selectionId, dataSource)
        {
        }

        protected override async Task<IResponse> GetComments(string next)
        {
            IsBusy = true;
            return await _dataSource.GetSelectionComments(_id);
        }
    }
}
