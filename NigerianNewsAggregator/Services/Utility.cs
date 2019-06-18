using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NigerianNewsAggregator.Services
{
    public class Utility
    {
        public string Url { get; set; }
        public string NewsSectionClass { get; set; }
        public string  TitleClass { get; set; }
        public string  SummaryClass { get; set; }
        public string DateClass { get; set; }
        public string  LinkClass { get; set; }
    }

    public class UtilityFunctions
    {
        public List<Utility> ListofSites()
        {          

            var sites = new Utility[]
            {
                new Utility
                {
                    Url = "https://www.vanguardngr.com/category/national-news/",
                    NewsSectionClass = ".rtp-listing-post",
                    TitleClass = ".entry-title",
                    SummaryClass = ".entry-content",
                    LinkClass = ".rtp-read-more-link",
                    DateClass = ".entry-date"
                },

                new Utility
                {
                    Url ="https://punchng.com/topics/latest-news/",
                    NewsSectionClass = ".items.col-sm-12",
                    TitleClass = ".seg-title",
                    SummaryClass = ".seg-summary",
                    DateClass = ".seg-time",
                    LinkClass = "a"
                },

                new Utility
                {
                    Url = "https://www.thisdaylive.com/",
                    NewsSectionClass = ".td_module_2.td_module_wrap.td-animation-stack",
                    TitleClass = ".entry-title.td-module-title",
                    SummaryClass =".td-excerpt",
                    DateClass = ".entry-date",
                    LinkClass ="a"
                },

                new Utility
                {
                    Url = "https://www.premiumtimesng.com/category/news/top-news?",
                    NewsSectionClass = ".a-story",
                    TitleClass = ".a-story-title",
                    SummaryClass = ".a-story-content",
                    DateClass = ".a-story-meta",
                    LinkClass = "a"
                },

                new Utility
                {
                    Url = "https://thenationonlineng.net/category/news/",
                    NewsSectionClass = ".jeg_post.jeg_pl_lg_2",
                    TitleClass = ".jeg_post_title",
                    SummaryClass = ".jeg_post_excerpt",
                    DateClass = ".jeg_meta_date",
                    LinkClass = "a"
                }
            };

            return sites.ToList();
        }
    }
}
