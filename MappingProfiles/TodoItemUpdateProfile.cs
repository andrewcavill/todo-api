using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoItemUpdateProfile : Profile
    {
        public TodoItemUpdateProfile()
        {
            CreateMap<TodoItemUpdateVm, TodoItem>();
        }
    }
}