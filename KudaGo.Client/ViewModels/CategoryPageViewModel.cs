using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace KudaGo.Client.ViewModels
{
    interface IFilterListener
    {
        void Update(CategoryPageViewModel categoryViewModel);
    }

    class CategoryPageViewModel : PropertyChangedBase, ICategoryNameProvider
    {
        private readonly ObservableCollection<CategoryNodeViewModel> _items;
        private readonly IDataSource _dataSource;
        private readonly IFilterListener _filterListeer;
        private bool _isBusy;
        private bool? _isFree;
        private CategoryNodeViewModel _selectedItem;

        public CategoryPageViewModel(IDataSource dataSource, IFilterListener filterListeer)
        {
            _dataSource = dataSource;
            _filterListeer = filterListeer;

            _items = new ObservableCollection<CategoryNodeViewModel>();

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                await Load();
            });
        }

        public ObservableCollection<CategoryNodeViewModel> Items
        {
            get { return _items; }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChanged(() => IsBusy);
            }
        }

        public bool? IsFree
        {
            get { return _isFree; }
            set
            {
                _isFree = value == false ? null : value;
                SelectedItem = null;
                NotifyOfPropertyChanged(() => IsFree);
            }
        }

        public CategoryNodeViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChanged(() => SelectedItem);
                if (_selectedItem == null)
                    return;

                NavigationHelper.GoBack();
                _filterListeer.Update(this);
            }
        }

        public async Task Load()
        {
            if (Items.Any())
                return;

            IsBusy = true;
            var items = await _dataSource.GetEventCategories();

            Items.Add(new AllCategoryNodeViewModel());
            foreach (var item in items.OrderBy(i => i.Name))
            {
                var node = new CategoryNodeViewModel(item);
                var exist = Items.FirstOrDefault(n => node.Name.Contains(n.Name));
                if (exist == null)
                {
                    Items.Add(node);
                    continue;
                }

                exist.Slug = exist.Slug + "," + node.Slug;
            }

            IsBusy = false;
        }

        public string GetName(string slug)
        {
            var first = Items.FirstOrDefault(i => i.Slug.Contains(slug));
            if (first == null)
                return string.Empty;

            return first.Name;
        }
    }
}
