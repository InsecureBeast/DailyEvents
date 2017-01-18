using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class CategoryPageViewModel : PropertyChangedBase
    {
        private readonly ObservableCollection<CategoryNodeViewModel> _items;
        private readonly DataSource _dataSource;
        private bool _isBusy;
        private bool _isFree;
        private CategoryNodeViewModel _selectedItem;

        public CategoryPageViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
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

        public bool IsFree
        {
            get { return _isFree; }
            set
            {
                _isFree = value;
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
            }
        }

        public async Task Load()
        {
            if (Items.Any())
                return;

            IsBusy = true;
            var items = await _dataSource.GetEventCategories();
            foreach (var item in items)
            {
                Items.Add(new CategoryNodeViewModel(item));
            }
            IsBusy = false;
        }
    }
}
