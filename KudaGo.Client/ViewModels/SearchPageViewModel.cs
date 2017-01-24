using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class SearchPageViewModel : PropertyChangedBase
    {
        private readonly DataSource _dataSource;

        public SearchPageViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
        }
    }
}
