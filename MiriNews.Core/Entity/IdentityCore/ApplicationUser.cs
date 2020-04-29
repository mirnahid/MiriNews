using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MiriNews.Core.Entity.IdentityCore
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBrith { get; set; }
        public ICollection<Post> AddedPosts { get; set; }
        public ICollection<Post> UpdatedPost { get; set; }
        
    }
}
