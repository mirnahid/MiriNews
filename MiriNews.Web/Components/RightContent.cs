using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Models;
using System.Linq;

namespace MiriNews.Web.Components
{
    public class RightContent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public RightContent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            var model = _unitOfWork.GetRepository<Post>()
                .Find(x => x.RightContent == true)
                .OrderByDescending(x => x.PublishDate)
                .Include(x => x.Category)
                .Select(x => new TrendingViewModel
                {
                    CategoryName = x.Category.CategoryName,
                    CoverPhoto = x.CoverPhoto,
                    Id = x.Id,
                    Title = x.Title
                })
                .Take(5)
                .ToList();

            return View(model);
        }
    }
}
