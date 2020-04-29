using System;
using System.Collections.Generic;

namespace MiriNews.Web.Models
{
    public class NewsDetailViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }

        public int Views { get; set; }

        public DateTime PublishDate { get; set; }

        public List<TrendingViewModel> RelatedNews { get; set; }

    }
}
