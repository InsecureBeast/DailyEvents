using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Events.Data
{
    public interface IEvent
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        IImage FirstImage { get; }
        IDate Daterange { get; }
    }

    internal class Event : IEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ImageImpl first_image { get; set; }
        public IImage FirstImage { get { return first_image; } }

        public DateImpl daterange { get; set; }
        public IDate Daterange { get { return daterange; }}
    }
}
