using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

public static class UseCustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                await context.Response.WriteAsync("Səhv!<br><br>\r\n");

                var error =  context.Features.Get<IExceptionHandlerPathFeature>();

                if (error?.Error is FileNotFoundException)
                {
                    await context.Response.WriteAsync("Məlumat tapılmadı!<br><br>\r\n");
                }

                await context.Response.WriteAsync("<a href=\"/Home/Error\">Əsas Səhifə</a><br>\r\n");
                await context.Response.WriteAsync("</body></html>\r\n");
                await context.Response.WriteAsync(new string(' ', 512));


                //if (error != null)
                //{
                //    var ex = error.Error;

                //    var errorModel = new ErrorViewModel();

                //    errorModel.Status = 500;

                //    errorModel.Errors.Add(ex.Message);
                //}
            });

        });
    }

}

