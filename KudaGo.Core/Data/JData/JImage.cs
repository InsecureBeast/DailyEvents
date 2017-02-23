namespace DailyEvents.Core.Data.JData
{
    class JImage
    {
        private JThumbnail _thumbnails;
        private string _image;

        public JImage()
        {
            Source = new JImageSource();
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                _thumbnails = new JThumbnail(_image);
            }
        }

        public JThumbnail Thumbnails
        {
            get { return _thumbnails; }
        }

        public JImageSource Source { get; set; }
    }

    class JThumbnail
    {
        public JThumbnail(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return;

            _144x96 = imageUrl.Replace("media/images", "media/thumbs/144x96/images");
            _640x384 = imageUrl.Replace("media/images", "media/thumbs/640x384/images");
        }
        public string _640x384 { get; private set; }
        public string _144x96 { get; private set; }
    }

    class JImageSource
    {
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
