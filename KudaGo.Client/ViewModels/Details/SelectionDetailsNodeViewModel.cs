using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Selections;
using KudaGo.Client.Extensions;
using KudaGo.Client.Model;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsNodeViewModel
    {
        public SelectionDetailsNodeViewModel(ISelectionItem item, IDataSource dataSource)
        {
            if (item == null)
                return;

            Image = item.FirstImage.Thumbnail.Small;
            NormalImage = item.FirstImage.Thumbnail.Normal;
            Title = item.Title.GetNormalString();
            Description = item.Description;
        }

        public string Image { get; private set; }
        public string NormalImage { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
    }
}
