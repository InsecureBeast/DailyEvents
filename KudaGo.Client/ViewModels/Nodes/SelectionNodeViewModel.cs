using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Selections;
using DailyEvents.Client.Extensions;
using DailyEvents.Client.Model;
using DailyEvents.Client.Helpers;

namespace DailyEvents.Client.ViewModels.Nodes
{
    internal class SelectionNodeViewModel : NodeViewModel
    {
        private readonly IDataSource _dataSource;
        public SelectionNodeViewModel(ISelectionListResult result, IDataSource dataSource )
        {
            if (result == null)
                return;

            _dataSource = dataSource;
            Id = result.Id;
            Title = result.Title.GetNormalString();

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
        public override long Id { get; protected set; }
        public override string Title { get; protected set; }
        public string Date { get; private set; }
    }
}
