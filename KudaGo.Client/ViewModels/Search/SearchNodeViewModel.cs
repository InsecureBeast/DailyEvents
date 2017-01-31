using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Search;
using KudaGo.Client.Common;
using KudaGo.Client.Extensions;

namespace KudaGo.Client.ViewModels.Search
{
    class SearchNodeViewModel : NodeViewModel
    {
        private string _image;

        public SearchNodeViewModel(ISearchResult result)
        {
            Type = result.CType;
            Id = result.Id;
            Title = result.Title.GetNormalString();
            Description = result.Description.GetNormalString();
            Place = result.Address;
            Categories = string.Empty;
            

            var image = result.FirstImage;
            if (image.Image != null)
                Image = image.Thumbnail.Normal;
            else
                LoadImage(result.ItemUrl);
        }

        private async void LoadImage(string itemUrl)
        {
            var image = await ImageLoader.LoadImage(itemUrl);
            Image = image;
        }

        public override long Id { get; protected set; }
        public CType Type { get; private set; }
        public string Place { get; private set; }
        public string Categories { get; private set; }
        public bool IsFree { get { return false; } }

        public string Image
        {
            get { return _image; }
            protected set
            {
                _image = value;
                NotifyOfPropertyChanged(() => Image);
            }
        }
        public string Description { get; protected set; }

        public override string ToString()
        {
            return Title + " : type = " + Type;
        }
    }
}
