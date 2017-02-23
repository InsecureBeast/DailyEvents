using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JData;

namespace DailyEvents.Core.Data
{
    public interface IGenre
    {
        int Id { get; }
        string Name { get; }
        string Slug { get; }
    }

    internal class Genre : IGenre
    {
        public Genre(JGenre jGenre)
        {
            if (jGenre == null)
                return;

            Id = jGenre.Id;
            Name = jGenre.Name;
            Slug = jGenre.Slug;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
    }
}
