using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.Common
{
    interface ISettingsChangeNotifier
    {
        void Subscribe(ISettingsChangeListener listener);
        void Unsubscribe(ISettingsChangeListener listener);
        void Notify();
    }

    interface ISettingsChangeListener
    {
        void UpdateSettings();
    }

    class SettingsChangeNotifier : ISettingsChangeNotifier
    {
        private readonly List<WeakReference> _listeners = new List<WeakReference>();

        public void Subscribe(ISettingsChangeListener listener)
        {
            _listeners.Add(new WeakReference(listener));
        }

        public void Unsubscribe(ISettingsChangeListener listener)
        {
            _listeners.RemoveAll(x => ReferenceEquals(listener, x.Target));
        }

        public void Notify()
        {
            foreach (var l in _listeners.ToArray())
            {
                var listener = l.Target as ISettingsChangeListener;
                if (listener != null)
                    listener.UpdateSettings();
                else
                    _listeners.Remove(l);
            }
        }
    }
}
