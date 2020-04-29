using MiriNews.Core.Entity.IdentityCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage ="istifadəçi adını qeyd edin")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="emaili daxil edin")]
        [EmailAddress(ErrorMessage ="email düzgün formatda deyil")]
        public string Email { get; set; }
        [Required(ErrorMessage ="adı daxil edin")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "soyadı daxil edin")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "doğum tarixini daxil edin")]
        public DateTime DateofBrith { get; set; }
        [Required(ErrorMessage = "şifrəni daxil edin")]
        public string  Password { get; set; }
        public string  roleName { get; set; }
        public List<ApplicationRole> Roles { get; set; }
    }
}
