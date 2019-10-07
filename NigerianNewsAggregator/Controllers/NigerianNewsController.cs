using Microsoft.AspNetCore.Mvc;
using NigerianNewsAggregator.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NigerianNewsAggregator.Controllers
{
    [Route("api/ngnews")]
    [ApiController]
    public class NigerianNewsController : ControllerBase
    {
        private readonly INewsAggregatorService _service;

        public NigerianNewsController(INewsAggregatorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                return Ok( await ListNewsAsync());
            }
            catch (Exception)
            {
                return NoContent();             
            }

        }

        private async Task<List<NewsInfo>> ListNewsAsync()
        {
            List<NewsInfo> newsAggregatedDb = new List<NewsInfo>();

            UtilityFunctions util = new UtilityFunctions();
            var noRSSFeedSites = util.ListofSites();
            var availableRSSFeedSites = new[] { "http://saharareporters.com/feeds/latest/feed" };


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

            return newsAggregatedDb;
        }
    }
}