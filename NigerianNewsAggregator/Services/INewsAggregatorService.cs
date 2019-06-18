using AngleSharp.Dom;
using NigerianNewsAggregator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NigerianNewsAggregator.Services
{
    public interface INewsAggregatorService
    {
        
        Task<List<NewsInfo>> GetNewsAsync(Utility siteInfo);

        List<NewsInfo> GetNewsFromRSS(string url);
    }
}
