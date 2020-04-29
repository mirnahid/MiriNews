using Microsoft.AspNetCore.Mvc;

namespace MiriNews.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Categories()
        {
            return View();
        }
    }
}