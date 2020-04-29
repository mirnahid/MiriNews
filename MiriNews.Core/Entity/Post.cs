using MiriNews.Core.Entity.IdentityCore;
using System;
using System.Collections.Generic;

namespace MiriNews.Core.Entity
{
    public class Post
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        public bool TopTrending { get; set; }
        public bool ButtomTrending { get; set; }
        public bool RightContent { get; set; }
        public int Views { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public string AddedUserId { get; set; }
        public ApplicationUser  AddUser { get; set; }
        public string  UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

    }
}
