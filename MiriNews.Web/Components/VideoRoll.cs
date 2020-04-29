using Microsoft.AspNetCore.Mvc;

namespace MiriNews.Web.Components
{
    public class VideoRoll:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
