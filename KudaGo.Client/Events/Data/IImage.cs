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
        public ImageImpl()
        {
            thumbnails = new Thumbnail();
            source = new ImageSource();
        }

        public string Image { get; set; }
        public Thumbnail thumbnails { get; set; }
        public IThumbnail Thumbnails
        {
            get { return thumbnails; }
        }
        public ImageSource source { get; set; }
        public IImageSource Source
        {
            get { return source; }
        }
    }

    class Thumbnail : IThumbnail
    {
        public string _640x384 { get; set; }
        public string _144x96 { get; set; }
    }

    class ImageSource : IImageSource
    {
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
