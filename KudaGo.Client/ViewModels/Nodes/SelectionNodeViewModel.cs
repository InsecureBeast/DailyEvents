using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Selections;
using KudaGo.Client.Extensions;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;

namespace KudaGo.Client.ViewModels.Nodes
{
    internal class SelectionNodeViewModel : NodeViewModel
    {
        private readonly DataSource _dataSource;
        public SelectionNodeViewModel(ISelectionListResult result, DataSource dataSource )
        {
            if (result == null)
                return;

            _dataSource = dataSource;
            //Task.Run(async ()=> await LoadImage(result.Id));

            Title = result.Title.GetNormalString();
            //Description = result.Description;

            if (result.PublicationDate.HasValue)
            {
                var format = ResourcesHelper.GetLocalizationString("PublishedAtStringFormat");
                Date = string.Format(format, result.PublicationDate.Value.ToString("g"));
            }
        }

        private async Task LoadImage(long selectionId)
        {
            var res = await _dataSource.GetSelectionDetails(selectionId);
            if (res == null)
                return;

            var image = res.Images.FirstOrDefault();
            if (image != null)
                Image = image.Thumbnail.Normal;
        }

        public string Image { get; private set; }
        public override string Title { get; protected set; }
        public string Date { get; private set; }
    }
}
