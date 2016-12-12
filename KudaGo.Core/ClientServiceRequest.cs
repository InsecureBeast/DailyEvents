using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KudaGo.Core
{
    public class ClientServiceRequest<TResponse>
    {
        public async Task<TResponse> ExecuteAsync(string url)
        {
            return await Request(url);
        }

        private async Task<TResponse> Request(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new ArgumentException("URL is not a valid!");

            var source = await HttpGetAsync(url);
            return ParseResponse(source);
        }

        /// <summary>
        /// Parses the response and deserialize the content into the requested response object.
        /// </summary>
        private TResponse ParseResponse(string response)
        {
            var deserializeObject = JsonConvert.DeserializeObject<TResponse>(response);
            var deserializeObject1 = JsonConvert.DeserializeObject<object>(response);
            return (TResponse)deserializeObject;
        }

        /// <exception cref="WebException">An error occurred while downloading the resource. </exception>
        private static async Task<string> HttpGetAsync(string uri)
        {
            const string botUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("User-Agent", botUserAgent);
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
