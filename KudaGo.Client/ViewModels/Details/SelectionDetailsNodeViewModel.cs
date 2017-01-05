using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Selections;
using KudaGo.Client.Extensions;
using KudaGo.Core.Data;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsNodeViewModel
    {
        public SelectionDetailsNodeViewModel(ISelectionItem item, DataSource dataSource)
        {
            if (item == null)
                return;

            Image = item.FirstImage.Thumbnail.Small;
            Title = item.Title.GetNormalString();
            Description = item.Description;

            //LayoutHelper.InvokeFromUiThread(async () => await LoadImages(item, dataSource));
        }

        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Place { get; private set; }

        private async Task LoadImages(ISelectionItem item, DataSource dataSource)
        {
            if (item.CType == "place")
            {
                var rs = await dataSource.GetPlaceDetails(item.Id);
                var image = rs.Images.FirstOrDefault();
                if (image == null)
                    return;

                Image = image.Thumbnail.Small;
            }

        }
    }
}
