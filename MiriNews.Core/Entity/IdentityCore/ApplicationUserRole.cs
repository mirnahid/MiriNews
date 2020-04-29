using Microsoft.AspNetCore.Identity;
namespace MiriNews.Core.Entity.IdentityCore
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}