using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ModelsDtos;

namespace WebApi.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationCreateDto>();
            CreateMap<ReservationCreateDto, Reservation>();

            CreateMap<Reservation, ReservationReadDto>();
            CreateMap<ReservationReadDto, Reservation>();
        }
    }
}
