namespace KudaGo.Core.Data.JData
{
    internal class JPlace
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Subway { get; set; }
        public string Location { get; set; }
        public bool Is_Stub { get; set; }
        public string Site_Url { get; set; }
        public JCoordinates Coords { get; set; }
        public bool Is_Closed { get; set; }
    }
}
