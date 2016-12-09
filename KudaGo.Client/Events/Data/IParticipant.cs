using System.Collections.Generic;

namespace KudaGo.Core.Events.Data
{
    public interface IParticipant
    {
        IRole Role { get; }
        IAgent Agent { get; }
    }

    public interface IRole
    {
        long Id { get; }
        string Name { get; }
        string Name_Plural { get; }
    }

    public interface IAgent : IResult
    {
        long Id { get; }
        string CType { get; }
        string Title { get; }
        string Slug { get; }
        string Description { get; }
        string Body_Text { get; }
        float Rank { get; }
        string Agent_Type { get; }
        IEnumerable<IImage> Images { get; }
        string Item_Url { get; }
        bool Disable_Comments { get; }
    }

    internal class Participant : IParticipant
    {
        public Role role { get; set; }
        public IRole Role { get { return role; } }

        public Agent agent { get; set; }
        public IAgent Agent { get { return agent; } }
    }

    internal class Role : IRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Name_Plural { get; set; }
    }

    internal class Agent : Result, IAgent
    {
        public Agent()
        {
            images = new ImageImpl[0];
        }

        public long Id { get; set; }
        public string CType { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Body_Text { get; set; }
        public float Rank { get; set; }
        public string Agent_Type { get; set; }

        public IEnumerable<ImageImpl> images { get; set; }
        public IEnumerable<IImage> Images { get { return images; }}

        public string Item_Url { get; set; }
        public bool Disable_Comments { get; set; }
    }
}
