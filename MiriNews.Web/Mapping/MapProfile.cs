using AutoMapper;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Web.Areas.Admin.Models;
using MiriNews.Web.Models;

namespace MiriNews.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Post, AddNewsViewModel>();

            CreateMap<AddNewsViewModel, Post>();
            
            CreateMap<Post, UpdateNewsViewModel>();

            CreateMap<UpdateNewsViewModel, Post>();

            CreateMap<NewsDetailViewModel, Post>();

            CreateMap<Post, NewsDetailViewModel>();

            CreateMap<Category, CategoryListViewModel>();

            CreateMap<CategoryListViewModel, Category>();

            CreateMap<Category, AddCategoryViewModel>();

            CreateMap<AddCategoryViewModel, Category>();

            CreateMap<ApplicationUser, AddUserViewModel>();

            CreateMap<AddUserViewModel, ApplicationUser>();

            CreateMap<ApplicationUser, UserListViewModel>();

            CreateMap<UserListViewModel, ApplicationUser>();
        }
    }
}
