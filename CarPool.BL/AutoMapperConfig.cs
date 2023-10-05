using AutoMapper;
using CarPool.BL.Models;
using CarPool.DAL.Entities;


namespace CarPool.BL
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<Car, CarAddModel>().ReverseMap();
            CreateMap<Car, CarDetailModel>().ReverseMap();
            CreateMap<ApplicationUser, UserDetailModel>().ReverseMap();
            CreateMap<ApplicationUser, UserAddModel>().ReverseMap();
            CreateMap<Ride, RideAddModel>().ReverseMap();
            CreateMap<Ride, RideResultModel>().ReverseMap();


        }
    }
}
