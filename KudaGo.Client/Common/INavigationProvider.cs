using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.Common
{
    interface INavigationProvider
    {
        void SetTitle(string title);
        //event EventHandler<TitleChangedEventArgs> TitleChanged;
    }

    class TitleChangedEventArgs : EventArgs
    {
        public TitleChangedEventArgs(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }
    }
}
