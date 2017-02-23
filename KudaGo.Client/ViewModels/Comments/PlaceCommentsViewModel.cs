using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using DailyEvents.Core.Data;

namespace DailyEvents.Client.ViewModels.Comments
{
    class PlaceCommentsViewModel : CommentsViewModel
    {
        public PlaceCommentsViewModel(long placeId, IDataSource dataSource) : base(placeId, dataSource)
        {
        }

        protected override async Task<IResponse> GetComments(string next)
        {
            IsBusy = true;
            return await _dataSource.GetPlaceComments(_id);
        }
    }
}
