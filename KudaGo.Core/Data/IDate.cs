﻿using DailyEvents.Core.Data.JData;
using System;

namespace DailyEvents.Core.Data
{
    public interface IDate
    {
        string StartDate { get; }
        string StartTime { get; }
        DateTime? Start { get; }
        string EndDate { get; }
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

            StartDate = jDate.Start_Date;
            StartTime = jDate.Start_Time;
            Start = DateTimeHelper.GetDateTimeFromUnixTime(jDate.Start);
            EndDate = jDate.End_Date;
            EndTime = jDate.End_Time;
            End = DateTimeHelper.GetDateTimeFromUnixTime(jDate.End);
            IsContinuous = jDate.Is_Continuous;
            IsEndless = jDate.Is_Endless;
            IsStartless = jDate.Is_Startless;
            UsePlaceSchedule = jDate.Use_Place_Schedule;
        }

        public string StartDate { get; private set; }
        public string StartTime { get; private set; } 
        public DateTime? Start { get; private set; }
        public string EndDate { get; private set; }
        public string EndTime { get; private set; }
        public DateTime? End { get; private set; }
        public bool IsContinuous { get; private set; }
        public bool IsEndless { get; private set; }
        public bool IsStartless { get; private set; }
        public bool UsePlaceSchedule { get; private set; }
    }
}
