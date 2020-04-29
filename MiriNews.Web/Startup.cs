using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiriNews.Core.Respositories;
using MiriNews.Core.Services;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Data;
using MiriNews.Data.Repositories;
using MiriNews.Data.UnitOfWorks;
using MiriNews.Service.Services;
using AutoMapper;
using MiriNews.Core.Entity.IdentityCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiriNews.Web.Filters;
using MiriNews.Web.WatherApiService;
using System;

namespace MiriNews.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient<WeatherApi>(options =>
            {
                options.BaseAddress = new Uri(Configuration["baseUrl"]);
            });

            services.AddScoped<PostNotFoundFilter>();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<MiriContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("MiriNews.Data")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IService<>), typeof(Service<>));

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                 config.Stores.MaxLengthForKeys = 128;
                 config.Password.RequireDigit = false;
                 config.Password.RequireLowercase = false;
                 config.Password.RequiredLength = 4;
                 config.Password.RequiredUniqueChars = 0;
                 config.Password.RequireLowercase = false;
                 config.Password.RequireNonAlphanumeric = false;
                 config.Password.RequireUppercase = false;                      
                
            })
              .AddEntityFrameworkStores<MiriContext>()
              .AddDefaultTokenProviders();

            services.AddSession();

            services.ConfigureApplicationCookie(configure =>
            {
                configure.Cookie.HttpOnly = true;

                configure.Cookie.IsEssential = true;

                configure.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                configure.LoginPath = "/admin/account/login";          
                
                configure.LogoutPath= "/admin/account/logout";

                //configure.Cookie = new CookieBuilder
                //{
                //    IsEssential = true
                //};
            });

            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //extension method  bug var hell olunmalidi


                //app.UseCustomException();

                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");



            });
        }
    }
}
