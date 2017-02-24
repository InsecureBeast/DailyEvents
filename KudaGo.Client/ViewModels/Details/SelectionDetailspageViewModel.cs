using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Extensions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DailyEvents.Client.Command;
using DailyEvents.Client.ViewModels.Comments;

namespace DailyEvents.Client.ViewModels.Details
{
    class SelectionDetailsPageViewModel : DetailsPageViewModel
    {
        private readonly ObservableCollection<SelectionDetailsNodeViewModel> _selections;
        private readonly DelegateCommand _selectCommand;

        public SelectionDetailsPageViewModel(long selectionId, string title, IDataSource dataSource) : base(selectionId, title, dataSource)
        {
            _selections = new ObservableCollection<SelectionDetailsNodeViewModel>();
            _selectCommand = new DelegateCommand(Select);
        }

        public ObservableCollection<SelectionDetailsNodeViewModel> Selections => _selections;

        public ICommand SelectCommand => _selectCommand;

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetSelectionDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                foreach (var item in rs.Items)
                {
                    _selections.Add(new SelectionDetailsNodeViewModel(item, _dataSource));
                }

                await CommentsViewModel.Load();
            });
        }

        protected override CommentsViewModel CreateCommentsViewModel()
        {
            return new SelectionCommentsViewModel(_id, _dataSource);
        }

        private void Select(object nodeViewModel)
        {
            var node = nodeViewModel as SelectionDetailsNodeViewModel;
            if (node == null)
                return;

            NavigationHelper.NavigateTo(typeof(DetailsPage), node);
        }
    }
}
