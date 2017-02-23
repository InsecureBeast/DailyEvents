using DailyEvents.Client.Extensions;
using DailyEvents.Client.Helpers;
using DailyEvents.Core.News;
using System.Linq;

namespace DailyEvents.Client.ViewModels.Nodes
{
    internal class NewsNodeViewModel : NodeViewModel
    {
        public NewsNodeViewModel(INewsListResult result)
        {
            if (result == null)
                return;

            var image = result.Images.FirstOrDefault();
            if (image != null)
                Image = image.Thumbnail.Normal;

            Id = result.Id;
            Title = result.Title.GetNormalString();
            Description = result.Description;

            if (result.Place != null)
                Place = result.Place.Title.GetNormalString();

            if (result.PublicationDate.HasValue)
            {
                var format = ResourcesHelper.GetLocalizationString("PublishedAtStringFormat");
                Date = string.Format(format, result.PublicationDate.Value.ToString("g"));
            }
        }

        public string Image { get; private set; }
        public override long Id { get; protected set; }
        public override string Title { get; protected set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
        public string Date { get; private set; }
    }
}