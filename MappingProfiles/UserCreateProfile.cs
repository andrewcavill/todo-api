using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class UserCreateProfile : Profile
    {
        public UserCreateProfile()
        {
            CreateMap<UserCreateVm, User>();
        }
    }
}