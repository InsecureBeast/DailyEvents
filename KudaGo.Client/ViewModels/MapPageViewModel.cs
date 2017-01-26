using KudaGo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class MapPageViewModel : PropertyChangedBase
    {
        public MapPageViewModel(ICoordinates coords, string title)
        {
            Location = coords;
            Title = title;
        }

        public ICoordinates Location
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }
    }
}
