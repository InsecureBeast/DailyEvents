using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data
{
    public interface IEvent
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        IImage FirstImage { get; }
        IDate DateRange { get; }
    }

    internal class Event : IEvent
    {
        public Event(JEvent jEvent)
        {
            if (jEvent == null)
            {
                FirstImage = new ImageImpl(new JImage());    
                DateRange = new DateImpl(new JDate());
                return;
            }

            Id = jEvent.Id;
            Title = jEvent.Title;
            Description = jEvent.Description;
            FirstImage = new ImageImpl(jEvent.First_Image);
            DateRange = new DateImpl(jEvent.Daterange);
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IImage FirstImage { get; private set; }
        public IDate DateRange { get; private set; }
    }
}
