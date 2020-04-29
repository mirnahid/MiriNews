using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class AddRoleViewModel
    {
        [Required(ErrorMessage ="rol adı daxil edilməlidir")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
