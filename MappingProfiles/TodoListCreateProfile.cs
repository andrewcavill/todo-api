using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoListCreateProfile : Profile
    {
        public TodoListCreateProfile()
        {
            CreateMap<TodoListCreateVm, TodoList>();
        }
    }
}