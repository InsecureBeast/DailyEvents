using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Selections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class SelectionsViewModel : SectionViewModel
    {
        private readonly DataSource _dataSource;

        public SelectionsViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        protected override async void AddData(IResponse response)
        {
            var res = response as ISelectionListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new SelectionNodeViewModel(result, _dataSource));
            }
            IsBusy = false;
        }

        protected override async Task<IResponse> GetData(string next)
        {
            IsBusy = true;
            return await _dataSource.GetSelections(next);
        }
    }
}
