﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Core.Data;

namespace KudaGo.Client.ViewModels.Comments
{
    class EventCommentsViewModel : CommentsViewModel
    {
        public EventCommentsViewModel(long id, IDataSource dataSource) : base(id, dataSource)
        {
        }

        protected override async Task<IResponse> GetComments(string next)
        {
            IsBusy = true;
            return await _dataSource.GetEventComments(_id);
        }
    }
}
