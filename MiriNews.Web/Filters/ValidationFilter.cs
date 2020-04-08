using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MiriNews.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Filters
{
    public class ValidationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var model = new ErrorViewModel();
                model.Status = 400;
                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(x => x.Errors);
                modelErrors.ToList().ForEach(x =>
                {
                    model.Errors.Add(x.ErrorMessage);
                });
                context.Result = new BadRequestObjectResult(model);
            }
            
        }
    }
}
