using DailyEvents.Client.Model;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Core.Data;
using DailyEvents.Core.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels
{
    class MoviesViewModel : SectionViewModel
    {
        private readonly IDataSource _dataSource;

        public MoviesViewModel(IDataSource dataSource)
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

            base.AddData(response);
        }

        protected override async Task<IResponse> GetData(string next)
        {
            return await _dataSource.GetMovies(next);
        }
    }
}
