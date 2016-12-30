using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Helpers
{
    class ResourcesHelper
    {
        public static string GetLocalizationString(string key)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            return loader.GetString(key);
        }
    }
}
