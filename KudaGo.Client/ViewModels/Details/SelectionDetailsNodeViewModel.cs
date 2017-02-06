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
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Search;
using KudaGo.Core;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsNodeViewModel : NodeViewModel
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
            Description = item.Description.StripHtmlTags();
            Type = item.CType.GetCType();
            Id = item.Id;
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

        public override string Title { get; protected set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
        public CType Type { get; private set; }
        public override long Id { get; protected set; }

        private async void LoadImage(string itemUrl)
        {
            string image = string.Empty;
            if (itemUrl.Contains("movie"))
                image = await ImageLoader.LoadMovieImage(itemUrl);
            else
                image = await ImageLoader.LoadImage(itemUrl);

            LayoutHelper.InvokeFromUiThread(() =>
            {
                Image = image;
            });
        }
    }
}
