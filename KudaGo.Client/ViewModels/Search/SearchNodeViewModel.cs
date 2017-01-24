using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Search;

namespace KudaGo.Client.ViewModels.Search
{
    class SearchNodeViewModel : NodeViewModel
    {
        private ISearchResult result;

        public SearchNodeViewModel(ISearchResult result)
        {
            this.result = result;
            Type = result.CType;
            Id = result.Id;
            Title = result.Title;
            Description = result.Description;
            var image = result.FirstImage;
            if (image != null)
                Image = image.Thumbnail.Normal;
        }

        public override long Id { get; protected set; }
        public CType Type { get; private set; }
        public string Image { get; protected set; }
        public string Description { get; protected set; }

        public override string ToString()
        {
            return Title + " : type = " + result.CType;
        }
    }
}
