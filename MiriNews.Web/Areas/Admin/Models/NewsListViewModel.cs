using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class NewsListViewModel
    {
        public string CategoryName { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime EndDate { get; set; }
        public ApplicationUser  Editor { get; set; }
        public bool onTrending { get; set; }
       

    }
}
