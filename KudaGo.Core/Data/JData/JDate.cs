namespace DailyEvents.Core.Data.JData
{
    internal class JDate
    {
        public string Start_Date { get; set; }
        public string Start_Time { get; set; }
        public long Start { get; set; }
        public string End_Date { get; set; }
        public string End_Time { get; set; }
        public long End { get; set; }
        public bool Is_Continuous { get; set; }
        public bool Is_Endless { get; set; }
        public bool Is_Startless { get; set; }
        public bool Use_Place_Schedule { get; set; }
    }
}
