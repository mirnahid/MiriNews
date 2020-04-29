using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Models;
using System.Linq;

namespace MiriNews.Web.Components
{
    public class WhatsNew:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public WhatsNew(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            //Kateqoriyaya görə sıralama


            //var model = new WhatsNewViewModel();

            //model.Fashion = _unitOfWork.GetRepository<Post>()
            //    .Find(x => x.CategoryId == 6)
            //    .Where(x=>DateTime.Now.Day-x.PublishDate.Day<=7&& DateTime.Now.Day - x.PublishDate.Day>=0)
            //    .Include(x => x.Category)
            //    .Select(x => new Base
            //    {
            //        CategoryName = x.Category.CategoryName,
            //        Id = x.Id,
            //        Title = x.Title,
            //        CoverPhoto=x.CoverPhoto
            //    }).Take(4)
            //    .ToList();

            //model.LifeStyle = _unitOfWork.GetRepository<Post>()
            //    .Find(x => x.CategoryId == 5)
            //    .Where(x => DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >=0)
            //    .Include(x => x.Category)
            //    .Select(x => new Base
            //    {
            //        CategoryName = x.Category.CategoryName,
            //        Id = x.Id,
            //        Title = x.Title,
            //        CoverPhoto=x.CoverPhoto
            //    }).Take(4)
            //    .ToList();

            //model.Political = _unitOfWork.GetRepository<Post>()
            //     .Find(x => x.CategoryId == 3)
            //     .Where(x => DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >=0)
            //     .Include(x => x.Category)
            //     .Select(x => new Base
            //     {
            //         CategoryName = x.Category.CategoryName,
            //         Id = x.Id,
            //         Title = x.Title,
            //         CoverPhoto=x.CoverPhoto
            //     }).Take(4)
            //     .ToList();

            //model.Sports = _unitOfWork.GetRepository<Post>()
            //    .Find(x => x.CategoryId == 4)
            //    .Where(x => DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >=0)
            //    .Include(x => x.Category)
            //    .Select(x => new Base
            //    {
            //        CategoryName = x.Category.CategoryName,
            //        Id = x.Id,
            //        Title = x.Title,
            //        CoverPhoto=x.CoverPhoto
            //    }).Take(4)
            //    .ToList();

            //model.Technology = _unitOfWork.GetRepository<Post>()
            //    .Find(x => x.CategoryId == 5)
            //    .Where(x => DateTime.Now.Day - x.PublishDate.Day <= 7 && DateTime.Now.Day - x.PublishDate.Day >=0)
            //    .Include(x => x.Category)
            //    .Select(x => new Base
            //    {
            //        CategoryName = x.Category.CategoryName,
            //        Id = x.Id,
            //        Title = x.Title,
            //        CoverPhoto=x.CoverPhoto
            //    }).Take(4)
            //    .ToList();

            var model = _unitOfWork.GetRepository<Post>()
                .GetAll()
                .Include(x => x.Category)
                .OrderByDescending(x => x.PublishDate)
                .Take(60)
                .Select(x => new Base
                {
                    CategoryName = x.Category.CategoryName,
                    Id = x.Id,
                    CoverPhoto = x.CoverPhoto,
                    Title = x.Title
                }).ToList();

            return View(model);
        }
    }
}
