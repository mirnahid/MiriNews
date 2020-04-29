using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Web.Areas.Admin.Models;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult RoleList()
        {
            var model = new UserRoleViewModel();
            model.Role = _roleManager.Roles.ToList();
            return View(model);
        }

       public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            var checkRole = await _roleManager.RoleExistsAsync(model.Name);
            if (checkRole==false)
            {                
                var result = await _roleManager.CreateAsync(new ApplicationRole { Name = model.Name });

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Roles");
                }

                ModelState.AddModelError("", "Səhvlik baş verdi");
            }

            ModelState.AddModelError("", "daxil etdiyiniz rol artıq var");
            return View();
        }

        public async Task<JsonResult> DeleteRole(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)            {
                 
                return Json(new ResultJson { Message = "Success", Status = true });
            }
            return Json(new ResultJson { Message = "Error", Status = false });
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                return View(new AddRoleViewModel { Name = role.Name, Id = role.Id });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(AddRoleViewModel model)
        {
            var checkRole = await _roleManager.RoleExistsAsync(model.Name);
            if (checkRole==false)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                ModelState.AddModelError("", "səhvlik baş verdi");
            }
            ModelState.AddModelError("", "daxil etdiyiniz rol artıq var");
            return View(model);
        }
    }
}