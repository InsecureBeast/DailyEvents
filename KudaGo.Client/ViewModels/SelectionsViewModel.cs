using DailyEvents.Client.Model;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Core.Data;
using DailyEvents.Core.Selections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels
{
    class SelectionsViewModel : SectionViewModel
    {
        private readonly IDataSource _dataSource;

        public SelectionsViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        protected override void AddData(IResponse response)
        {
            var res = response as ISelectionListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new SelectionNodeViewModel(result, _dataSource));
            }

            base.AddData(response);
        }

        protected override async Task<IResponse> GetData(string next)
        {
            return await _dataSource.GetSelections(next);
        }
    }
}
