using System;
using System.Collections.Generic;
using System.Linq;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Places
{
    public interface IPlaceListResponse : IResponse
    {
        IEnumerable<IPlaceListResult> Results { get; }
    }

    public interface IPlaceListResult : IPlace
    {
        bool HasParkingLot { get; }
    }

    internal class PlaceListResponse : IPlaceListResponse
    {
        public PlaceListResponse(JPlaceListResponse jPlaceList)
        {
            if (jPlaceList == null)
            {
                Results = new IPlaceListResult[0];
                return;
            }

            Count = jPlaceList.Count;
            Next = jPlaceList.Next;
            Previous = jPlaceList.Previous;
            Results = jPlaceList.Results.Select(r => new PlaceListResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IPlaceListResult> Results { get; private set; }
    }

    internal class PlaceListResult : Place, IPlaceListResult
    {
        public PlaceListResult(JPlaceListResult jPlaceListResult) : base(jPlaceListResult)
        {
            if (jPlaceListResult == null)
                return;

            HasParkingLot = jPlaceListResult.Has_Parking_Lot;
        }
       
        public bool HasParkingLot { get; private set; }
    }
}
