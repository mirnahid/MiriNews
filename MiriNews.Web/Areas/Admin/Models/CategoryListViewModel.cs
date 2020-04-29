using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class CategoryListViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Kateqoriya adını daxil edin")]
        public string CategoryName { get; set; }
    }
}
