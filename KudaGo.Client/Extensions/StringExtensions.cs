using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.Extensions
{
    static class StringExtensions
    {
        public static string GetNormalString(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                return @string;

            var normalTitle = @string.ToCharArray();
            if (Char.IsLower(@string[0]))
            {
                normalTitle[0] = Char.ToUpper(@string[0]);
                return new string(normalTitle);
            }

            return @string;
        }

        public static string StripHtmlTags(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return "";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }
    }
}
