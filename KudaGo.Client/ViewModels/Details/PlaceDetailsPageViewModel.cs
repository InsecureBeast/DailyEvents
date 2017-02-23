using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Extensions;
using DailyEvents.Core.Data;
using DailyEvents.Client.Command;
using DailyEvents.Client.ViewModels.Comments;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.ViewModels.Details
{
    class PlaceDetailsPageViewModel : DetailsPageViewModel
    {
        private Uri _source;
        private string _metro;
        private ICoordinates _location;
        private string _adress;
        private string _phone;
        private string _timetable;
        private bool _isClosed;
        private readonly DelegateCommand _mapCommand;
        private readonly DelegateCommand _callCommand;

        public PlaceDetailsPageViewModel(long id, IDataSource dataSource) : base(id, dataSource)
        {
            _mapCommand = new DelegateCommand(MapOpen);
            _callCommand = new DelegateCommand(Call);
        }

        public Uri Source
        {
            get { return _source; }
            private set
            {
                _source = value;
                NotifyOfPropertyChanged(() => Source);
            }
        }

        public string Metro
        {
            get { return _metro; }
            private set
            {
                _metro = value;
                NotifyOfPropertyChanged(() => Metro);
            }
        }

        public string Adress
        {
            get { return _adress; }
            private set
            {
                _adress = value;
                NotifyOfPropertyChanged(() => Adress);
            }
        }

        public string Phone
        {
            get { return _phone; }
            private set
            {
                _phone = value;
                NotifyOfPropertyChanged(() => Phone);
            }
        }

        public string Timetable
        {
            get { return _timetable; }
            private set
            {
                _timetable = value;
                NotifyOfPropertyChanged(() => Timetable);
            }
        }

        public bool IsClosed
        {
            get { return _isClosed; }
            private set
            {
                _isClosed = value;
                NotifyOfPropertyChanged(() => IsClosed);
            }
        }

        public ICoordinates Location
        {
            get { return _location; }
            private set
            {
                _location = value;
                NotifyOfPropertyChanged(() => Location);
            }
        }

        public ICommand MapCommand
        {
            get { return _mapCommand; }
        }

        public ICommand CallCommand
        {
            get { return _callCommand; }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetPlaceDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description.GetNormalString();
                BodyText = rs.BodyText.GetNormalString();
                Adress = rs.Address;
                Phone = rs.Phone;
                Timetable = rs.Timetable;
                IsClosed = rs.IsClosed;

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                Metro = rs.Subway;
                Location = rs.Coords;

                await CommentsViewModel.Load();
            });
        }

        protected override CommentsViewModel CreateCommentsViewModel()
        {
            return new PlaceCommentsViewModel(_id, _dataSource);
        }

        private void MapOpen(object obj)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
                return;

            frame.Navigate(typeof(MapPage), new MapPageViewModel(Location, Title, _dataSource));
        }

        private void Call(object obj)
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(Phone, Title);
            }
        }
    }
}
