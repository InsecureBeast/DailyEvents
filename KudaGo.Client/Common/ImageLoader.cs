using HtmlAgilityPack;
using DailyEvents.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyEvents.Client.Common
{
    class ImageLoader
    {
        public static async Task<string> LoadImage(string itemUrl)
        {
            //var page = await ClientServiceRequest<string>.HttpGetAsync(itemUrl);
            //var content = await page.Content.ReadAsStringAsync();

            //var regex = new Regex(@"https:\/\/kudago\.com\/media\/images(.*[""])");
            //var colm = regex.Matches(content);
            //foreach (Match match in colm)
            //{
            //    //og: image" content="https://kudago.com/media/images/place/0e/f3/0ef3c3d09a7202e7fd3dc28337085b3e.jpg" />
            //    return match.Value.Substring(0, match.Value.Length - 1);
            //}
            //return string.Empty;

            if (string.IsNullOrEmpty(itemUrl))
                return string.Empty;
            
            Task<string> t = Task<string>.Factory.StartNew(() =>
            {
                try
                {
                    var content = GetContent(itemUrl);
                    var source = WebUtility.HtmlDecode(content.Result);
                    return source;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }, TaskCreationOptions.LongRunning);

            var doc = await t;
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(doc);

            var imgs = document.DocumentNode.Descendants().Where(x => x.Name == "img");// ToArray()[1].Attributes.First(n => n.Value == "post-big-preview-image");
            var imgNode = imgs.FirstOrDefault(i => i.Attributes.ToList().Exists(a => a.Value == "post-big-preview-image"));
            if (imgNode == null)
                return string.Empty;

            var res = imgNode.Attributes.FirstOrDefault(a => a.Name == "src");
            if (res == null)
                return string.Empty;

            return ApiService.SITE_BASE_URL + res.Value;
        }

        public static async Task<string> LoadMovieImage(string itemUrl)
        {
            if (string.IsNullOrEmpty(itemUrl))
                return string.Empty;

            Task<string> t = Task<string>.Factory.StartNew(() =>
            {
                var content = GetContent(itemUrl);
                var source = WebUtility.HtmlDecode(content.Result);
                return source;
            });

            var doc = await t;
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(doc);

            var links = document.DocumentNode.Descendants("span");
            var link = GetLink(links);
            return ApiService.SITE_BASE_URL + link;
        }

        private static async Task<string> GetContent(string itemUrl)
        {
            var page = await ClientServiceRequest<string>.HttpGetAsync(itemUrl);
            var content = await page.Content.ReadAsStringAsync();
            return content;
        }

        private static string GetLink(IEnumerable<HtmlNode> links)
        {
            foreach (var item in links)
            {
                if (item.Attributes["data-future-href"] == null)
                    continue;

                if (item.Attributes["class"] == null)
                    continue;

                if (item.Attributes["class"].Value != "single-content-frame-preview")
                    continue;

                return item.Attributes["data-future-href"].Value;
            }

            return string.Empty;
        }
    }
}
