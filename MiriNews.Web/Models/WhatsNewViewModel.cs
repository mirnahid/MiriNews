using System.Collections.Generic;

namespace MiriNews.Web.Models
{
    public class WhatsNewViewModel
    {
        public List<Base> Political { get; set; }
        public List<Base> LifeStyle { get; set; }
        public List<Base> Fashion { get; set; }
        public List<Base> Sports { get; set; }
        public List<Base> Technology { get; set; }

    }

     public  class Base
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CoverPhoto { get; set; }
    }
}
