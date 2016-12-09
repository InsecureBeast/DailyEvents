namespace KudaGo.Core.Events.Data
{
    public interface IDate
    {
        string Start_Date { get; }
        string Start_Time { get; }
        long Start { get; }
        string End_Date { get; }
        string End_Time { get; }
        long End { get; }
        bool Is_Continuous { get; }
        bool Is_Endless { get; }
        bool Is_Startless { get; }
        bool Use_Place_Schedule { get; }
    }

    internal class DateImpl : IDate
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
