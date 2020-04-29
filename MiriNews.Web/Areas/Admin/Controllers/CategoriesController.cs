using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiriNews.Core.Entity;
using MiriNews.Core.Services;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Web.Areas.Admin.Models;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService<Category> _service;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IService<Category> service)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult CategoryList()
        {            
            var category = _service.GetAll()
                .Select(x => new CategoryListViewModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName
                });          
            return View(category);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            await _service.AddAsync(_mapper.Map<Category>(model));

            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model!=null)
            {
                return View(_mapper.Map<CategoryListViewModel>(model));
            }
            ModelState.AddModelError("", "Səhvlik baş verdi");

            return View();
        }

        [HttpPost]
        public IActionResult Update(CategoryListViewModel model)
        {
            _service.Update(_mapper.Map<Category>(model));

            return RedirectToAction("CategoryList");
        }

        public async Task<JsonResult> DeleteCategory(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model!=null)
            {
                _service.Remove(model);
                return Json(new ResultJson { Message = "Success", Status = true });
            }
            return Json(new ResultJson { Message = "Error", Status = false });

        }
    }
}