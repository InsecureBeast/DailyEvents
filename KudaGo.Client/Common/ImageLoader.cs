using KudaGo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KudaGo.Client.Common
{
    class ImageLoader
    {
        public static async Task<string> LoadImage(string itemUrl)
        {
            var page = await ClientServiceRequest<string>.HttpGetAsync(itemUrl);
            var content = await page.Content.ReadAsStringAsync();

            var regex = new Regex(@"https:\/\/kudago\.com\/media\/images(.*[""])");
            var colm = regex.Matches(content);
            foreach (Match match in colm)
            {
                //og: image" content="https://kudago.com/media/images/place/0e/f3/0ef3c3d09a7202e7fd3dc28337085b3e.jpg" />
                return match.Value.Substring(0, match.Value.Length - 1);
            }

            return string.Empty;
        }
    }
}
