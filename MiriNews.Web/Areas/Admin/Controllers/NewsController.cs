using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Core.Services;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Areas.Admin.Extensions;
using MiriNews.Web.Areas.Admin.Models;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles = "Admin", AuthenticationSchemes = "amdin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService<Post> _service;
        private readonly IMapper _mapper;

        public NewsController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IService<Post> service)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _service = service;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNews()
        {
            var model = new AddNewsViewModel();
            model.Categories =  _unitOfWork.GetRepository<Category>().GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsViewModel model)
        {
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/news/coverphoto", model.Photo.FileName);
            var photoUrl = "images/news/coverphoto/" + model.Photo.FileName;
            using (var stream = new FileStream(path,FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
                model.CoverPhoto = photoUrl;
            }
            model.Category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.catId);
            model.AddUser = user;
            
            var post = _mapper.Map<Post>(model);

            await _unitOfWork.GetRepository<Post>().AddAsync(post);
             _unitOfWork.Commit();
            /*user.AddedPosts.Add(post)*/;
            return RedirectToAction("NewsList");
        }

        [Authorize(Roles ="admin")]
        public IActionResult NewsList()
        {
            var model = _unitOfWork.GetRepository<Post>()
                .GetAll()
                .Include(x => x.Category)
                .Include(x => x.AddUser)
                .Select(x => new NewsListViewModel
                {
                    CategoryName = x.Category.CategoryName,
                    Title = x.Title,
                    PublishedDate = x.PublishDate,
                    onTrending = x.ButtomTrending,
                    Id = x.Id,
                    Editor = x.AddUser,
                    UpdateUser = x.UpdateUser
                }).ToList();


            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteNews(int id)
        {
            var model = await _unitOfWork.GetRepository<Post>().GetByIdAsync(id);

            if (model!=null)
            {
                _unitOfWork.GetRepository<Post>().Remove(model);
                await _unitOfWork.CommitAsync();
                return Json(new ResultJson() { Message = "Success", Status = true });
            }
            return Json(new ResultJson() { Message = "Error", Status = false });
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateNews(int id)
        {
            var news = await _service.GetByIdAsync(id);

            if (news!=null)
            {
                var model = _mapper.Map<UpdateNewsViewModel>(news);
                model.Categories = _unitOfWork.GetRepository<Category>().GetAll();
                return View(model);
            }

            ModelState.AddModelError("", "səhvlik baş verdi");

            return RedirectToAction("NewsList");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateNews(UpdateNewsViewModel model)
        {
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            var adduser = await _userManager.FindByIdAsync(model.AddedUserId);
            model.AddedUserId = null;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/news/coverphoto", model.Photo.FileName);
            var photoUrl = "images/news/coverphoto/" + model.Photo.FileName;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
                model.CoverPhoto = photoUrl;
            }
            model.Category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.catId);
            model.UpdateUser = user;
            model.AddUser = adduser;
            var post = _mapper.Map<Post>(model);
            _service.Update(post);

            return RedirectToAction("NewsList");
        }

         public IActionResult UserNews(string id)
        {
            var posts = _service.Find(x => x.AddedUserId == id)
                .Include(x => x.Category)
                .Include(x => x.AddUser)
                .Select(x => new NewsListViewModel
                {
                    CategoryName = x.Category.CategoryName,
                    Title = x.Title,
                    PublishedDate = x.PublishDate,
                    onTrending = x.ButtomTrending,
                    Id = x.Id,
                    Editor = x.AddUser,
                    UpdateUser = x.UpdateUser
                }).ToList();

            return View(posts);
        }

        public IActionResult AutoNews()
        {
            var bot = new Auto();
            bot.AutoPost("https://oxu.az/criminal/383818");

            return View();
        }
    }
    
}