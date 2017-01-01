using System.Collections.Generic;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    internal class JEventListResult : JResult
    {
        public JEventListResult()
        {
            Images = new List<JImage>();
            Dates = new JDate[0];
            Place = new JPlace();
            Location = new JLocation();
            Participants = new List<JParticipant>();
            Categories = new string[0];
        }

        public long Id { get; set; }
        public long Publication_Date { get; set; }
        public IEnumerable<JDate> Dates { get; set; }
        public string Title { get; set; }
        public string Short_Title { get; set; }
        public string Slug { get; set; }
        public JPlace Place { get; set; }
        public string Description { get; set; }
        public string Body_Text { get; set; }
        public JLocation Location { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string Tagline { get; set; }
        public string Age_Restriction { get; set; }
        public string Price { get; set; }
        public bool Is_Free { get; set; }
        public IEnumerable<JImage> Images { get; set; }
        public string Site_Url { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<JParticipant> Participants { get; set; }
    }
}
