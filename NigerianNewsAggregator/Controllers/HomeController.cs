using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NigerianNewsAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NigerianNewsAggregator.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        //public List<NewsInfo> News = new List<NewsInfo>();
        private readonly INewsAggregatorService _service;

        public HomeController(INewsAggregatorService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            List<NewsInfo> newsAggregatedDb = new List<NewsInfo>();

            //if (!_cache.TryGetValue("Aggregated-news", out newsAggregatedDb))
            //{
            UtilityFunctions util = new UtilityFunctions();
            var noRSSFeedSites = util.ListofSites();
            var availableRSSFeedSites = new[] { "http://saharareporters.com/feeds/latest/feed" };

            try
            {
                ViewData["Error"] = "";
                foreach (var site in noRSSFeedSites)
                {
                    var aggregrateNews = await _service.GetNewsAsync(site);
                    newsAggregatedDb.AddRange(aggregrateNews);
                }
                foreach (var site in availableRSSFeedSites)
                {
                    var aggregateNews = _service.GetNewsFromRSS(site);
                    newsAggregatedDb.AddRange(aggregateNews);
                }
            }
            catch (Exception)
            {
                ViewData["Error"] = "Kindly wait some minutes, then try reloading the page, there seems to be an error in connection ";
            }


            //    _cache.Set("Aggregated-news", newsAggregatedDb);
            //}

            return View(newsAggregatedDb.OrderBy(m=> m.Date));
        }

    }


    public class NewsInfo
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Link { get; set; }
        public string Date { get; set; }
    }


}