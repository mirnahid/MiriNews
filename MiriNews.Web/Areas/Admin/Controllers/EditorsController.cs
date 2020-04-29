using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Core.Services;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Areas.Admin.Models;
using MiriNews.Web.Filters;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class EditorsController : Controller
    {
        private readonly IService<Post> _service;

        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;



        public EditorsController(IService<Post> service, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;

        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddNews()
        {
            var model = new AddNewsViewModel();
            model.Categories = _unitOfWork.GetRepository<Category>().GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/news/coverphoto", model.Photo.FileName);
            var photoUrl = "images/news/coverphoto/" + model.Photo.FileName;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
                model.CoverPhoto = photoUrl;
            }
            model.Category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.catId);
            model.AddUser = user;

            var post = _mapper.Map<Post>(model);

            await _service.AddAsync(post);
            return RedirectToAction("NewsList");
        }

        public async Task<IActionResult> NewsList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = _service.Find(x => x.AddedUserId == user.Id)
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

        [ServiceFilter(typeof(PostNotFoundFilter))]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var news = await _service.GetByIdAsync(id);

            if (String.Equals(news.AddedUserId, user.Id))
            {
                _service.Remove(news);
                return Json(new ResultJson() { Message = "Success", Status = true });
            }
            return Json(new ResultJson() { Message = "Error", Status = false });
        }

        [ServiceFilter(typeof(PostNotFoundFilter))]
        public async Task<IActionResult> UpdateNews(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var news = await _service.GetByIdAsync(id);

            if (String.Equals(news.AddedUserId, user.Id))
            {
                var model = _mapper.Map<UpdateNewsViewModel>(news);
                model.Categories = _unitOfWork.GetRepository<Category>().GetAll();
                return View(model);
            }

            ModelState.AddModelError("", "səhvlik baş verdi");

            return RedirectToAction("NewsList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNews(UpdateNewsViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
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
    }
}