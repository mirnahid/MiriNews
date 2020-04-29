using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage ="Kateqoriya adını daxil edin")]
        public string CategoryName { get; set; }
    }
}
