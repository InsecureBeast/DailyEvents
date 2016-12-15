using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    class JSelectionDetailsResponse : JResult
    {
        public JSelectionDetailsResponse()
        {
            Images = new List<JImage>();
            Items = new List<JSelectionItem>();
        }

        public long Id { get; set; }
        public string CType { get; set; }
        public long Publication_Date { get; set; }
        public string Title { get; set; }
        public IEnumerable<JSelectionItem> Items { get; set; }
        public IEnumerable<JImage> Images { get; set; }
        public string Description { get; set; }
        public string Body_Text { get; set; }
        public string Site_Url { get; set; }
        public string Slug { get; set; }
    }

    internal class JSelectionItem : JResult
    {
        public JSelectionItem()
        {
            Place = new JPlace();
            Daterange = new JDate();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CType { get; set; }
        public JPlace Place { get; set; }
        public JDate Daterange { get; set; }
    }
}
