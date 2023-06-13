using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsScraper.Scraping
{
    public class Scraper
    {
        public static List<Post> Scrape()
        {
            var html = GetNewsHtml();
            return ParseNewsHtml(html);
        }

        private static List<Post> ParseNewsHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".td-module-container.td-category-pos-image");
            var posts = new List<Post>();
            foreach (var div in resultDivs)
            {
                var post = new Post();
                
                var titleSpan = div.QuerySelector(".entry-title.td-module-title");

                if (titleSpan == null)
                {
                    continue;
                }
                if (titleSpan != null)
                {
                    post.Title = titleSpan.TextContent;
                }

                var text = div.QuerySelector(".td-excerpt");

                if (text == null)
                {
                    continue;
                }
                if (text != null)
                {
                    post.Text = text.TextContent;
                }

                var imageTag = div.QuerySelector(".td-module-thumb");
                if (imageTag != null)
                {
                    post.ImageUrl = imageTag.QuerySelector("span").Attributes["data-img-url"].Value;
                }

                var linkTag = div.QuerySelector("h3");
                if (linkTag != null)
                {
                    post.Link = linkTag.QuerySelector("a").Attributes["href"].Value;
                }

                var comments = div.QuerySelector(".td-module-comments").QuerySelector("a");
                if (comments != null)
                {
                    post.Comments = int.Parse(comments.TextContent);
                }

                posts.Add(post);
            }
            return posts;
        }

        private static string GetNewsHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = "https://thelakewoodscoop.com/";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
