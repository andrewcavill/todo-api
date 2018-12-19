using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoListUpdateProfile : Profile
    {
        public TodoListUpdateProfile()
        {
            CreateMap<TodoListUpdateVm, TodoList>();
        }
    }
}