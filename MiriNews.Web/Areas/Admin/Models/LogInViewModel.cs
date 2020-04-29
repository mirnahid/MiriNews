using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "istifadəçi adı daxil edilməlidir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "şifrə daxil edilməlidir")]
        public string Password { get; set; }
    }
}
