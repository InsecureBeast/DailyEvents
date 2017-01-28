using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Selections;
using KudaGo.Client.Extensions;
using KudaGo.Client.Model;
using KudaGo.Client.Common;
using KudaGo.Client.Helpers;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsNodeViewModel : PropertyChangedBase
    {
        private string _image;

        public SelectionDetailsNodeViewModel(ISelectionItem item, IDataSource dataSource)
        {
            if (item == null)
                return;

            var image = item.FirstImage;
            if (image.Image != null)
                Image = image.Thumbnail.Small;
            else
                LoadImage(item.ItemUrl);

            Title = item.Title.GetNormalString();
            Description = item.Description;
        }

        public string Image
        {
            get { return _image; }
            protected set
            {
                _image = value;
                NotifyOfPropertyChanged(() => Image);
            }
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Place { get; private set; }

        private async void LoadImage(string itemUrl)
        {
            var image = await ImageLoader.LoadImage(itemUrl);
            LayoutHelper.InvokeFromUiThread(() =>
            {
                Image = image;
            });
        }
    }
}
