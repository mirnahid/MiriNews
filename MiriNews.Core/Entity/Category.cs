using System.Collections.Generic;

namespace MiriNews.Core.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
