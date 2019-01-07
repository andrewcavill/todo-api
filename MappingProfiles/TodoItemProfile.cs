using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemVm>();
        }
    }
}