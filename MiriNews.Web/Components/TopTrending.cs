using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.UnitOfWorks;
using System.Linq;
using MiriNews.Web.Models;

namespace MiriNews.Web.Components
{
    public class TopTrending:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopTrending(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IViewComponentResult Invoke()
        {
            var model = _unitOfWork.GetRepository<Post>()
                .Find(x => x.TopTrending == true)
                .OrderByDescending(x => x.PublishDate)
                .Include(x => x.Category)
                .Select(x => new TrendingViewModel
                {
                    CategoryName = x.Category.CategoryName,
                    CoverPhoto = x.CoverPhoto,
                    Id = x.Id,
                    Title = x.Title
                }).First();

            return View(model);
        }
    }
}
