using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KudaGo.Client.Command;
using KudaGo.Client.ViewModels.Nodes;

namespace KudaGo.Client.ViewModels.Details
{
    class SelectionDetailsPageViewModel : DetailsPageViewModel
    {
        private readonly ObservableCollection<SelectionDetailsNodeViewModel> _selections;
        private readonly DelegateCommand _selectCommand;

        public SelectionDetailsPageViewModel(long selectionId, IDataSource dataSource) : base(selectionId, dataSource)
        {
            _selections = new ObservableCollection<SelectionDetailsNodeViewModel>();
            _selectCommand = new DelegateCommand(Select);
        }

        public ObservableCollection<SelectionDetailsNodeViewModel> Selections
        {
            get { return _selections; }
        }

        public ICommand SelectCommand
        {
            get { return _selectCommand; }
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

        private void Select(object nodeViewModel)
        {
            var node = nodeViewModel as SelectionDetailsNodeViewModel;
            if (node == null)
                return;

            if (node.Type == Core.Search.CType.ListItem)
                return;

            NavigationHelper.NavigateTo(typeof(DetailsPage), node);
        }
    }
}
