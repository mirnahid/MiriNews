using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Filters;
using MiriNews.Web.Models;

namespace MiriNews.Web.Controllers
{
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

        [ServiceFilter(typeof(PostNotFoundFilter))]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _unitOfWork.GetRepository<Post>()
                .GetByIdAsync(id);
            post.Views++;
            _unitOfWork.GetRepository<Post>().Update(post);
            await _unitOfWork.CommitAsync();
            var model = _mapper.Map<NewsDetailViewModel>(post);


            model.RelatedNews = _unitOfWork.GetRepository<Post>()
                .Find(x => x.CategoryId == post.CategoryId&&x.Id!=post.Id && DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >= 0)
                .Include(x => x.Category)
                .Select(x => new TrendingViewModel
                { 
                   Title=x.Title,
                   CategoryName=x.Category.CategoryName,
                   Id=x.Id,
                   CoverPhoto=x.CoverPhoto
                }).ToList();

            return View(model);
        }

        public IActionResult GetByCategory(int id)
        {
            var model = _unitOfWork.GetRepository<Post>()
                .Find(x => x.CategoryId == id)
                .OrderByDescending(x=>x.PublishDate)
                .Include(x => x.Category)
                .Select(x => new TrendingViewModel
                {
                     CategoryName=x.Category.CategoryName,
                     CoverPhoto=x.CoverPhoto,
                     Id=x.Id,
                     Title=x.Title
                }).ToList();

            return View(model);
        }
    }
}