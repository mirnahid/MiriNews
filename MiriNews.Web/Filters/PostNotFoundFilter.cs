using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiriNews.Core.Entity;
using MiriNews.Core.Services;
using MiriNews.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Filters
{
    public class PostNotFoundFilter:ActionFilterAttribute
    {
        private readonly IService<Post> _postService;

        public PostNotFoundFilter(IService<Post> postService)
        {
            _postService = postService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments.Values.FirstOrDefault();

            try
            {
                var post = await _postService.GetByIdAsync((int)id);
                if (post != null)
                {
                    await next();
                }
                else
                {
                    var model = new ErrorViewModel();

                    model.Status = 400;
                    model.Errors.Add("Seçilən xəbər tapılmadı");
                    context.Result = new RedirectToActionResult("Error", "Home", model);
                }
            }
            catch (Exception)
            {

                var model = new ErrorViewModel();

                model.Status = 400;
                model.Errors.Add("Seçilən xəbər tapılmadı");
                context.Result = new RedirectToActionResult("Error", "Home", model);
            }  

        }
    }
}
