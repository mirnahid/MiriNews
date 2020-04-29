using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Models;
using System;
using System.Linq;

namespace MiriNews.Web.Components
{
    public class WeeklyTopNews:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeeklyTopNews(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  IViewComponentResult Invoke()
        {
            var post = _unitOfWork.GetRepository<Post>()
                .GetAll()
                .Where(x => DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >=0)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Views)
                .Select(x => new TrendingViewModel
                {
                    CategoryName = x.Category.CategoryName,
                    CoverPhoto = x.CoverPhoto,
                    Id = x.Id,
                    Title = x.Title
                })
                .Take(4)
                .ToList();

            return View(post);
        }
    }
}
