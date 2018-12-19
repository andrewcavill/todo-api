using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListVm>();
        }
    }
}