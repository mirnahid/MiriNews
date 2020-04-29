using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("editor"))
            {
                return RedirectToAction("Index", "Editors");
            }
            return View();
        }       
        
    }
}