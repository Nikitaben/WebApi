using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ModelsDtos;

namespace WebApi.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemCreateDto>();
            CreateMap<MenuItemCreateDto, MenuItem>();

            CreateMap<MenuItem, MenuItemReadDto>();
            CreateMap<MenuItemReadDto, MenuItem>();
        }
    }
}
