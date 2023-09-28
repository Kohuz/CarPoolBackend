using AutoMapper;
using CarPool.BL.Models;
using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<Car, CarAddModel>().ReverseMap();
            CreateMap<Car, CarDetailModel>().ReverseMap();
            CreateMap<User, UserDetailModel>().ReverseMap();
            CreateMap<User, UserAddModel>().ReverseMap();
            CreateMap<Ride, RideAddModel>().ReverseMap();
            CreateMap<Ride, RideResultModel>().ReverseMap();


        }
    }
}
