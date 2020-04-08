using System;
using System.Collections.Generic;
using System.Text;

namespace MiriNews.Core.Entity
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
