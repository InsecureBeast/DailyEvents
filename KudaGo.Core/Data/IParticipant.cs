using System.Collections.Generic;
using System.Linq;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
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
        string NamePlural { get; }
    }

    public interface IAgent : IResult
    {
        long Id { get; }
        string CType { get; }
        string Title { get; }
        string Slug { get; }
        string Description { get; }
        string BodyText { get; }
        float Rank { get; }
        string AgentType { get; }
        IEnumerable<IImage> Images { get; }
        string ItemUrl { get; }
        bool DisableComments { get; }
    }

    internal class Participant : IParticipant
    {
        public Participant(JParticipant jParticipant)
        {
            if (jParticipant == null)
            {
                Role = new Role(new JRole());
                Agent = new Agent(new JAgent());
                return;
            }
            
            Role = new Role(jParticipant.Role);
            Agent = new Agent(jParticipant.Agent);
        }

        public IRole Role { get; private set; }
        public IAgent Agent { get; private set; }
    }

    internal class Role : IRole
    {
        public Role(JRole role)
        {
            if (role == null)
                return;

            Id = role.Id;
            Name = role.Name;
            NamePlural = role.Name_Plural;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string NamePlural { get; private set; }
    }

    internal class Agent : Result, IAgent
    {
        public Agent(JAgent jAgent) : base(jAgent)
        {
            if (jAgent == null)
            {
                Images = new IImage[0];
                return;
            }

            Id = jAgent.Id;
            CType = jAgent.CType;
            Title = jAgent.Title;
            Slug = jAgent.Slug;
            Description = jAgent.Description;
            BodyText = jAgent.Body_Text;
            Rank = jAgent.Rank;
            AgentType = jAgent.Agent_Type;
            Images = jAgent.Images.Select(i => new ImageImpl(i));
            ItemUrl = jAgent.Item_Url;
            DisableComments = jAgent.Disable_Comments;
        }

        public long Id { get; private set; }
        public string CType { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string BodyText { get; private set; }
        public float Rank { get; private set; }
        public string AgentType { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public string ItemUrl { get; private set; }
        public bool DisableComments { get; private set; }
    }
}
