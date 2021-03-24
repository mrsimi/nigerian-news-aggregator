using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using AngleSharp;
using NigerianNewsAggregator.Controllers;

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


            foreach (var item in newsContent)
            {
                var eachNews = new NewsInfo()
                {
                    Title = item.QuerySelector(siteInfo.TitleClass).TextContent.ToUpper(),
                    Summary = item.QuerySelector(siteInfo.SummaryClass).TextContent,
                    Link = item.QuerySelector(siteInfo.LinkClass).GetAttribute("href"),
                    Date = item.QuerySelector(siteInfo.DateClass).TextContent
                };
                
                newsInfoDb.Add(eachNews);
            }

            return newsInfoDb;
        }

        public List<NewsInfo>GetNewsFromRSS(string url)
        {
            List<NewsInfo> newsInfoDb = new List<NewsInfo>();
            Regex rxHtml = new Regex("<[^>]+>|&nbsp;");
            int maxLength = 80;

            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();            

            foreach (SyndicationItem item in feed.Items)
            {
                var eachNews = new NewsInfo()
                {
                    Title = item.Title.Text,
                    Summary = rxHtml.Replace( item.Summary.Text, "").Substring(0, maxLength),
                    Date = item.PublishDate.ToString(),
                    Link = item.Links[0].Uri.ToString()
            };

                newsInfoDb.Add(eachNews);
            }

            return newsInfoDb;

        }
    }
}
