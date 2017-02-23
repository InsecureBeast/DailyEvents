using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Selections
{
    public interface ISelectionDetailsResponse
    {
        long Id { get; }
        string CType { get; }
        long PublicationDate { get; }
        string Title { get; }
        IEnumerable<ISelectionItem> Items { get; }
        IEnumerable<IImage> Images { get; }
        string Description { get; }
        string BodyText { get; }
        string SiteUrl { get; }
        string Slug { get; }
    }

    public interface ISelectionItem
    {
        long Id { get; }
        string Title { get; }
        string Description { get; }
        string CType { get; }
        IPlace Place { get; }
        IDate Daterange { get; }
        IImage FirstImage { get; }
        string ItemUrl { get; }
    }

    class SelectionDetailsResponse : ISelectionDetailsResponse
    {
        public SelectionDetailsResponse(JSelectionDetailsResponse jResult)
        {
            Images = new IImage[0];
            Items = new ISelectionItem[0];

            if (jResult == null)
                return;

            Id = jResult.Id;
            CType = jResult.CType;
            PublicationDate = jResult.Publication_Date;
            Title = jResult.Title;
            Description = jResult.Description;
            BodyText = jResult.Body_Text;
            SiteUrl = jResult.Site_Url;
            Slug = jResult.Slug;
            if (jResult.Images != null)
                Images = jResult.Images.Select(i => new ImageImpl(i));

            if (jResult.Items != null)
                Items = jResult.Items.Select(i => new SelectionItem(i));
        }

        public long Id { get; private set; }
        public string CType { get; private set; }
        public long PublicationDate { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<ISelectionItem> Items { get; private set; }
        public IEnumerable<IImage> Images { get; private set; }
        public string Description { get; private set; }
        public string BodyText { get; private set; }
        public string SiteUrl { get; private set; }
        public string Slug { get; private set; }
    }

    internal class SelectionItem : ISelectionItem
    {
        public SelectionItem(JSelectionItem jSelectionItem)
        {
            if (jSelectionItem == null)
            {
                Place = new Place(null);
                Daterange = new DateImpl(null);
                FirstImage = new ImageImpl(null);
                return;
            }

            Id = jSelectionItem.Id;
            Title = jSelectionItem.Title;
            Description = jSelectionItem.Description;
            CType = jSelectionItem.CType;
            Place = new Place(jSelectionItem.Place);
            Daterange = new DateImpl(jSelectionItem.Daterange);
            FirstImage = new ImageImpl(jSelectionItem.first_image);
            ItemUrl = jSelectionItem.Item_url;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string CType { get; private set; }
        public IPlace Place { get; private set; }
        public IDate Daterange { get; private set; }
        public IImage FirstImage { get; private set; }
        public string ItemUrl { get; private set; }
    }
}
