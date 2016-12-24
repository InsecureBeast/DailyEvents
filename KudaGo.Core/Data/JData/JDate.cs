namespace KudaGo.Core.Data.JData
{
    internal class JDate
    {
        public long Start_Date { get; set; }
        public string Start_Time { get; set; }
        public long Start { get; set; }
        public long End_Date { get; set; }
        public string End_Time { get; set; }
        public long End { get; set; }
        public bool Is_Continuous { get; set; }
        public bool Is_Endless { get; set; }
        public bool Is_Startless { get; set; }
        public bool Use_Place_Schedule { get; set; }
    }
}
