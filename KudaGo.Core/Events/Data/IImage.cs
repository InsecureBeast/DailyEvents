namespace KudaGo.Core.Events.Data
{
    public interface IImage
    {
        string Image { get; }
        IThumbnail Thumbnails { get; }
        IImageSource Source { get; }
    }

    public interface IImageSource
    {
        string Link { get; }
        string Name { get; }
    }

    public interface IThumbnail
    {
        string _640x384 { get; }
        string _144x96 { get; }
    }

    class ImageImpl : IImage
    {
        private Thumbnail _thumbnails;
        private string _image;

        public ImageImpl()
        {
            source = new ImageSource();
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                _thumbnails = new Thumbnail(_image);
            }
        }

        public IThumbnail Thumbnails
        {
            get { return _thumbnails; }
        }
        public ImageSource source { get; set; }
        public IImageSource Source
        {
            get { return source; }
        }
    }

    class Thumbnail : IThumbnail
    {
        public Thumbnail(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return;

            _144x96 = imageUrl.Replace("media/images", "media/thumbs/144x96/images");
            _640x384 = imageUrl.Replace("media/images", "media/thumbs/640x384/images");
        }
        public string _640x384 { get; private set; }
        public string _144x96 { get; private set; }
    }

    class ImageSource : IImageSource
    {
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
