using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace KudaGo.Client.Helpers
{
    public static class LayoutHelper
    {
        public static void InvokeFromUiThread(Action action)
        {
            Task.Run(async () =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync
                (
                    CoreDispatcherPriority.Normal,
                    () => action()
                );
            });
        }
    }
}
