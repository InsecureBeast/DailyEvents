using KudaGo.Client.Extensions;
using KudaGo.Core.News;
using System.Linq;

namespace KudaGo.Client.ViewModels.Nodes
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

            Title = result.Title.GetNormalString();
            //Description = result.Description;

            if (result.Place != null)
                Place = result.Place.Title.GetNormalString();

            if (result.PublicationDate.HasValue)
            {
                Date = result.PublicationDate.Value.ToString("g");
            }
        }

        public string Image { get; private set; }
        public override string Title { get; protected set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
        public string Date { get; private set; }
    }
}