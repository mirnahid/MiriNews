using AutoMapper;
using MiriNews.Core.Entity;
using MiriNews.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Post, AddNewsViewModel>();
            CreateMap<AddNewsViewModel, Post>();
        }
    }
}
