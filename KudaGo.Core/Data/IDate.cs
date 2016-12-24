using KudaGo.Core.Data.JData;
using System;

namespace KudaGo.Core.Data
{
    public interface IDate
    {
        DateTime? StartDate { get; }
        string StartTime { get; }
        DateTime? Start { get; }
        DateTime? EndDate { get; }
        string EndTime { get; }
        DateTime? End { get; }
        bool IsContinuous { get; }
        bool IsEndless { get; }
        bool IsStartless { get; }
        bool UsePlaceSchedule { get; }
    }

    internal class DateImpl : IDate
    {
        public DateImpl(JDate jDate)
        {
            if (jDate == null)
                return;

            StartDate = DateTimeHelper.GetDateTimeFromUnixTime(jDate.Start_Date);
            StartTime = jDate.Start_Time;
            Start = DateTimeHelper.GetDateTimeFromUnixTime(jDate.Start);
            EndDate = DateTimeHelper.GetDateTimeFromUnixTime(jDate.End_Date);
            EndTime = jDate.End_Time;
            End = DateTimeHelper.GetDateTimeFromUnixTime(jDate.End);
            IsContinuous = jDate.Is_Continuous;
            IsEndless = jDate.Is_Endless;
            IsStartless = jDate.Is_Startless;
            UsePlaceSchedule = jDate.Use_Place_Schedule;
        }

        public DateTime? StartDate { get; private set; }
        public string StartTime { get; private set; } 
        public DateTime? Start { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string EndTime { get; private set; }
        public DateTime? End { get; private set; }
        public bool IsContinuous { get; private set; }
        public bool IsEndless { get; private set; }
        public bool IsStartless { get; private set; }
        public bool UsePlaceSchedule { get; private set; }
    }
}
