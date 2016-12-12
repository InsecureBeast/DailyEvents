using System.Collections.Generic;

namespace KudaGo.Core.Data.JData
{
    internal class JParticipant
    {
        public JRole Role { get; set; }
        public JAgent Agent { get; set; }
    }

    internal class JRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Name_Plural { get; set; }
    }

    internal class JAgent : JResult
    {
        public JAgent()
        {
            Images = new JImage[0];
        }

        public long Id { get; set; }
        public string CType { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Body_Text { get; set; }
        public float Rank { get; set; }
        public string Agent_Type { get; set; }
        public IEnumerable<JImage> Images { get; set; }
        public string Item_Url { get; set; }
        public bool Disable_Comments { get; set; }
    }
}
