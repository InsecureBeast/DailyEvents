using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class MoviesViewModel : SectionViewModel
    {
        private readonly DataSource _dataSource;

        public MoviesViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        protected override void AddData(IResponse response)
        {
            var res = response as IMovieListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new MovieNodeViewModel(result));
            }
            IsBusy = false;
        }

        protected override async Task<IResponse> GetData(string next)
        {
            IsBusy = true;
            return await _dataSource.GetMovies(next);
        }
    }
}
