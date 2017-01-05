using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;
using System.Collections.ObjectModel;
using KudaGo.Core.Selections;
using KudaGo.Core.Data;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsPageViewModel : DetailsPageViewModel
    {
        private readonly ObservableCollection<SelectionDetailsNodeViewModel> _selections;
        public SelectionDetailsPageViewModel(long selectionId, DataSource dataSource) : base(selectionId, dataSource)
        {
            _selections = new ObservableCollection<SelectionDetailsNodeViewModel>();
        }

        public ObservableCollection<SelectionDetailsNodeViewModel> Selections
        {
            get { return _selections; }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetSelectionDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(() =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                var ids = rs.Items.Select(i => i.Id);
                

                foreach (var item in rs.Items)
                {
                    _selections.Add(new SelectionDetailsNodeViewModel(item, _dataSource));
                }

                //await EventCommentsViewModel.Load();
            });
        }

        private async void LoadImages(IEnumerable<ISelectionItem> items)
        {
            var placeBuilder = new StringBuilder();
            var eventBuilder = new StringBuilder();
            var newsBuilder = new StringBuilder();

            foreach (var item in items)
            {
                if (item.CType == "place")
                    placeBuilder.Append(item.Id + ",");

                if (item.CType == "event")
                    eventBuilder.Append(item.Id + ",");

                if (item.CType == "news")
                    newsBuilder.Append(item.Id + ",");
            }

            var places = await _dataSource.GetPlaceImages(placeBuilder.ToString());
            var events = await _dataSource.GetEventImages(eventBuilder.ToString());
            var news = await _dataSource.GetNewsImages(newsBuilder.ToString());
        }
    }
}
