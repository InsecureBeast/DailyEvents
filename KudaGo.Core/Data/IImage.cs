using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
{
    public interface IImage
    {
        string Image { get; }
        IThumbnail Thumbnail { get; }
        IImageSource Source { get; }
    }

    public interface IImageSource
    {
        string Link { get; }
        string Name { get; }
    }

    public interface IThumbnail
    {
        string Normal { get; }
        string Small { get; }
    }

    class ImageImpl : IImage
    {
        public ImageImpl(JImage jImage)
        {
            if (jImage == null)
            {
                Source = new ImageSource(new JImageSource());
                Thumbnail = new Thumbnail(null);
                return;
            }

            Image = jImage.Image;
            Source = new ImageSource(jImage.Source);
            Thumbnail = new Thumbnail(jImage.Thumbnails);
        }

        public string Image { get; private set; }
        public IThumbnail Thumbnail { get; private set; }
        public IImageSource Source { get; private set; }
    }

    class Thumbnail : IThumbnail
    {
        public Thumbnail(JThumbnail jThumbnail)
        {
            if (jThumbnail == null)
                return;

            Small = jThumbnail._144x96;
            Normal = jThumbnail._640x384;
        }
        public string Normal { get; private set; }
        public string Small { get; private set; }
    }

    class ImageSource : IImageSource
    {
        public ImageSource(JImageSource source)
        {
            Link = source.Link;
            Name = source.Name;
        }

        public string Link { get; private set; }
        public string Name { get; private set; }
    }
}
