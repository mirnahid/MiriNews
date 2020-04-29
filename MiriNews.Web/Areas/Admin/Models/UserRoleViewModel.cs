using MiriNews.Core.Entity.IdentityCore;
using System.Collections.Generic;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class UserRoleViewModel
    {
        public List<ApplicationUser> User { get; set; }
        public List<ApplicationRole> Role { get; set; }
    }
}
