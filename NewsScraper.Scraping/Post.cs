using System;

namespace NewsScraper.Scraping
{
    public class Post
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        public int Comments { get; set; }
        public string ImageUrl { get; set; }

    }
}
