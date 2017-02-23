namespace DailyEvents.Core.Data.JData
{
    internal class JEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JImage First_Image { get; set; }
        public JDate Daterange { get; set; }
    }
}
