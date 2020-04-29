using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Areas.Admin.Models;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;


        public UsersController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult AddUser()
        {
            var model = new AddUserViewModel();
            model.Roles = _roleManager.Roles.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            //var user = new ApplicationUser();
            //user.Email = model.Email;
            //user.FirstName = model.FirstName;
            //user.LastName = model.LastName;
            //user.DateOfBrith = model.DateofBrith;

            var result =await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.roleName);
                if (model.roleName=="admin")
                {
                    return RedirectToAction("AdminList");
                }
                return RedirectToAction("EditorList");
            }

            ModelState.AddModelError("", "səhvlik baş verdi");
            return View(model);
        }

        public async Task<IActionResult>EditorList()
        {
            var users = new List<UserListViewModel>();
            var user = await _userManager.GetUsersInRoleAsync("editor");

            foreach (var item in user)
            {
                var model = _mapper.Map<UserListViewModel>(item);
                model.AddPosts = _unitOfWork.GetRepository<Post>().Find(x => x.AddedUserId == item.Id).Count();
                users.Add(model);
            }
            //var users = _userManager.Users
            //    .Select(x => new UserRoleViewModel
            //    {
            //        User = x.UserRoles.Select(j => j.User).ToList(),
            //        Role = x.UserRoles.Select(j => j.Role).ToList()
            //    });

            ////var model = _userManager.Users
            ////    .Include(x => x.UserRoles)
            ////    .Select(x=> new UserListViewModel
            ////    {
            ////        Id=x.Id,
            ////        Name=x.FirstName,
            ////        Email=x.Email,
            ////        roleName=x.UserRoles.Select(x=>x.Role.Name).First(),
            ////        newsCount=x.AddedPosts.Count()

            ////    }).ToList();           



            return View(users);
        }

        public async Task<IActionResult> AdminList()
        {
            var users = new List<UserListViewModel>();
            
            var user = await _userManager.GetUsersInRoleAsync("admin");

            foreach (var item in user)
            {
                var model = _mapper.Map<UserListViewModel>(item);
                model.AddPosts = _unitOfWork.GetRepository<Post>().Find(x => x.AddedUserId == item.Id).Count();
                users.Add(model);
            }

            return View(users);
        }

        public async Task<JsonResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user.UserName==User.Identity.Name)
            {
                return Json(new ResultJson { Message = "Error", Status = false });
            }

            if (user!=null)
            {
                var result =await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(new ResultJson { Message = "Success", Status = true });
                }
                return Json(new ResultJson { Message = "Error", Status = false });
            }
            return Json(new ResultJson { Message = "Error", Status = false });
        }
    }
}