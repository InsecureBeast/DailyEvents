namespace DailyEvents.Core.Data.JData
{
    internal class JLocation
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public JCoordinates Coords { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
    }

    internal class JCoordinates
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
    }
}
