using Microsoft.AspNetCore.Http;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class UpdateNewsViewModel
    {
        public int Id { get; set; }
        public string AddedUserId { get; set; }
        public int Views { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [Required(ErrorMessage = "Xəbərin başlığını qeyd edin")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Xəbəri daxil edin")]
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        [Required(ErrorMessage = "Xəbərin başlıq şəklini seçin")]
        public IFormFile Photo { get; set; }
        public bool TopTrending { get; set; }
        public bool ButtomTrending { get; set; }
        public bool RightContent { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Kateqoriyanı seçin")]
        public int catId { get; set; }

        public ApplicationUser AddUser { get; set; }
        public ApplicationUser UpdateUser { get; set; }
    }
}
