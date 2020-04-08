using Microsoft.AspNetCore.Http;
using MiriNews.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class AddNewsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        [Required(ErrorMessage ="Xəbərin başlığını qeyd edin")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Xəbəri daxil edin")]
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        [Required(ErrorMessage ="Xəbərin başlıq şəklini seçin")]
        public IFormFile Photo { get; set; }
        public bool TopTrending { get; set; }
        public bool ButtomTrending { get; set; }
        public bool RightContent { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage ="Kateqoriyanı seçin")]
        public int catId { get; set; }
    }
}
