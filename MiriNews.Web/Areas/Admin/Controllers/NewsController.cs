using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Areas.Admin.Models;
using MiriNews.Web.Filters;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNews()
        {
            var model = new AddNewsViewModel();
            model.Categories =  _unitOfWork.GetRepository<Category>().GetAllAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsViewModel model)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/news/coverphoto", model.Photo.FileName);
            var photoUrl = "images/news/coverphoto/" + model.Photo.FileName;
            using (var stream = new FileStream(path,FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
                model.CoverPhoto = photoUrl;
            }
            model.Category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.catId);            
            var post = _mapper.Map<Post>(model);            
            await _unitOfWork.GetRepository<Post>().AddAsync(post);
            await _unitOfWork.CommitAsync();
            return RedirectToAction("NewsList");
        }

        public IActionResult NewsList()
        {
            var model = _unitOfWork.GetRepository<Post>().GetAllAsync()
                .Include(x => x.Category)
                .Include(x => x.AddUser)
                .Select(x => new NewsListViewModel
                {
                    CategoryName=x.Category.CategoryName,
                    Title=x.Title,
                    PublishedDate=x.PublishDate,
                    onTrending=x.ButtomTrending
                }).ToList();


            return View(model);
        }
    }
    
}