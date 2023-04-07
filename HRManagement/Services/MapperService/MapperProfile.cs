using AutoMapper;
using HRManagement.Models;
using HRManagement.ViewModels;

namespace HRManagement.Services.MapperService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserViewModel>()
                .ForMember(dest => dest.Id, t => t.MapFrom(m => m.Id)).ReverseMap();

            CreateMap<UserModel, UserLoginResponseViewModel>()
                .ForMember(dest => dest.Id, t => t.MapFrom(m => m.Id)).ReverseMap();
        }
    }
}
