using System;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace Elmah.Io.FaviconLoader
{
    public class Favicon : IFavicon
    {
        public Uri Load(Uri url)
        {
            if (!url.IsAbsoluteUri)
            {
                throw new ArgumentException("Only absolute URLs supported", "url");
            }

            var faviconIcoUrl = RequestFaviconIco(url);
            if (faviconIcoUrl != null) return faviconIcoUrl;

            var faviconPageUrl = RequestPage(url);
            if (faviconPageUrl != null) return faviconPageUrl;

            return null;
        }

        private Uri RequestPage(Uri url)
        {
            var html = new WebClient().DownloadString(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // If no <link rel="icon" ...> found just return null;
            var elements = htmlDocument.DocumentNode.SelectNodes("//link[contains(@rel, 'icon')]");
            if (elements == null || !elements.Any()) return null;

            // If favicon link was found, but no href specified just return null;
            var favicon = elements.First();
            var href = favicon.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(href)) return null;

            // If not a valid url just return null
            Uri faviconUrl;
            if (!Uri.TryCreate(href, UriKind.RelativeOrAbsolute, out faviconUrl)) return null;

            // If relative, force absolute url
            if (!faviconUrl.IsAbsoluteUri)
            {
                faviconUrl = new Uri(url, faviconUrl);
            }

            // Return favicon url if it can be requested.
            return Exists(faviconUrl) ? faviconUrl : null;
        }

        private Uri RequestFaviconIco(Uri baseUrl)
        {
            var faviconIcoUrl = new Uri(baseUrl, "favicon.ico");
            return Exists(faviconIcoUrl) ? faviconIcoUrl : null;
        }

        private bool Exists(Uri url)
        {
            var webRequest = HttpWebRequest.Create(url);
            webRequest.Method = "HEAD";
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            return webResponse.StatusCode == HttpStatusCode.OK;
        }
    }

    public interface IFavicon
    {
        Uri Load(Uri url);
    }
}
