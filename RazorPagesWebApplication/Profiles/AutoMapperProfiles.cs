using AutoMapper;
using DataLibrary.Models;
using RazorPagesWebApplication.Models;

namespace RazorPagesWebApplication.Profiles
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<OrderModel, OrderViewModel>().ReverseMap();
            CreateMap<FoodModel, FoodViewModel>().ReverseMap();
        }
    }
}
