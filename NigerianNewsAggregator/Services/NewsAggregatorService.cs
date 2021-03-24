using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using AngleSharp;
using NigerianNewsAggregator.Controllers;
using NigerianNewsAggregator.Models;

namespace NigerianNewsAggregator.Services
{
    public class NewsAggregatorService : INewsAggregatorService
    {
        public async Task<List<NewsInfo>> GetNewsAsync(Utility siteInfo)
        {
            List<NewsInfo> newsInfoDb = new List<NewsInfo>();

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(siteInfo.Url);
            var newsContent = document.QuerySelectorAll(siteInfo.NewsSectionClass);

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);     

            foreach (var item in newsContent)
            {
                var summary = item.QuerySelector(siteInfo.SummaryClass).TextContent.Trim();
                summary = Regex.Replace(summary,@"\s+", " ");
                var title = item.QuerySelector(siteInfo.TitleClass).TextContent.Trim();
                title = Regex.Replace(title, @"\s+", " ");
                var eachNews = new NewsInfo()
                {
                    Title = Regex.Replace(title, @"\t|\n|\r", ""),
                    Summary = Regex.Replace(summary, @"\t|\n|\r", ""),
                    Link = item.QuerySelector(siteInfo.LinkClass).GetAttribute("href"),
                    Date = item.QuerySelector(siteInfo.DateClass).TextContent,
                    Source = siteInfo.Source
                };
                
                newsInfoDb.Add(eachNews);
            }

            return newsInfoDb;
        }

        public List<NewsInfo>GetNewsFromRSS(string url, string source)
        {
            List<NewsInfo> newsInfoDb = new List<NewsInfo>();
            Regex rxHtml = new Regex("<[^>]+>|&nbsp;");
            int maxLength = 80;

            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();            

            foreach (SyndicationItem item in feed.Items)
            {
                var summary =  rxHtml.Replace(item.Summary.Text, "").Substring(0, maxLength).Trim();
                 summary = Regex.Replace(summary,@"\s+", " ");
                var title = item.Title.Text.Trim();
                title = Regex.Replace(title, @"\s+", " ");
                var eachNews = new NewsInfo()
                {
                    Title = Regex.Replace(title, @"\t|\n|\r", ""),
                    Summary = Regex.Replace(summary, @"\t|\n|\r", ""),
                    Date = item.PublishDate.ToString(),
                    Link = item.Links[0].Uri.ToString()
            };

                newsInfoDb.Add(eachNews);
            }

            return newsInfoDb;

        }
    }
}
