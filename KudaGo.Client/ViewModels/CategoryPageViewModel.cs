using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace KudaGo.Client.ViewModels
{
    interface IFilterListener
    {
        void Update(CategoryPageViewModel categoryViewModel);
    }

    class CategoryPageViewModel : PropertyChangedBase, ICategoryNameProvider
    {
        private readonly ObservableCollection<CategoryNodeViewModel> _items;
        private readonly NavigationViewModel _navigationViewModel;
        private readonly IDataSource _dataSource;
        private readonly IFilterListener _filterListeer;
        private bool _isBusy;
        private bool? _isFree;
        private CategoryNodeViewModel _selectedItem;
        private bool _isToday;
        private bool _isWeekend;
        private bool _isTomorrow;

        public CategoryPageViewModel(IDataSource dataSource, IFilterListener filterListeer)
        {
            _dataSource = dataSource;
            _filterListeer = filterListeer;

            _items = new ObservableCollection<CategoryNodeViewModel>();
            _navigationViewModel = new NavigationViewModel(dataSource);

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                try
                {
                    await Load();
                }
                catch (HttpRequestException)
                {
                    IsBusy = false;
                }
                catch (WebException)
                {
                    IsBusy = false;
                }
            });
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
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
                NotifyOfPropertyChanged(() => IsFree);
                Update();
            }
        }

        public CategoryNodeViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChanged(() => SelectedItem);
                Update();
            }
        }

        public void Update()
        {
            if (SelectedItem == null)
                return;

            NavigationHelper.GoBack();
            _filterListeer.Update(this);
        }

        public bool IsToday
        {
            get { return _isToday; }
            set
            {
                _isToday = value;
                if (_isToday)
                {
                    IsTomorrow = false;
                    IsWeekend = false;
                    Update();
                }
                NotifyOfPropertyChanged(() => IsToday);
            }
        }

        public bool IsWeekend
        {
            get { return _isWeekend; }
            set
            {
                _isWeekend = value;
                if (_isWeekend)
                {
                    IsToday = false;
                    IsTomorrow = false;
                    Update();
                }
                NotifyOfPropertyChanged(() => IsWeekend);
            }
        }

        public bool IsTomorrow
        {
            get { return _isTomorrow; }
            set
            {
                _isTomorrow = value;
                if (_isTomorrow)
                {
                    IsToday = false;
                    IsWeekend = false;
                    Update();
                }
                NotifyOfPropertyChanged(() => IsTomorrow);
            }
        }

        public FilterDefinition FilterDefinition
        {
            get { return GetFilterDefinition(); }
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
            var items = Items.Skip(1);
            var first = items.FirstOrDefault(i => i.Slug.Contains(slug));
            if (first == null)
                return string.Empty;

            return first.Name;
        }

        private DateTime GetToday()
        {
            return DateTime.Today;
        }

        private DateTime GetTommorow()
        {
            return DateTime.Today + TimeSpan.FromDays(1);
        }

        private IEnumerable<DateTime> GetWeekend()
        {
            var end = DateTime.Today + TimeSpan.FromDays(5);
            var days = new List<DateTime>();
            for (var date = DateTime.Today; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                    days.Add(date);
            }
            return days;
        }

        private FilterDefinition GetFilterDefinition()
        {
            var definition = new FilterDefinition();
            definition.IsFree = _isFree;

            if (SelectedItem != null)
                definition.Categories = SelectedItem.Slug;

            if (IsToday)
            {
                definition.ActualSince = GetToday();
                definition.ActualUntil = GetToday();
            }
            if (IsTomorrow)
            {
                definition.ActualSince = GetTommorow();
                definition.ActualUntil = GetTommorow();
            }
            if (IsWeekend)
            {
                var weekend = GetWeekend().ToArray();
                definition.ActualSince = weekend[0];
                definition.ActualUntil = weekend[1];
            }

            return definition;
        }
    }
}
