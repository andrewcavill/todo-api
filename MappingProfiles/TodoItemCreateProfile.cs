using AutoMapper;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.MappingProfiles
{
    public class TodoItemCreateProfile : Profile
    {
        public TodoItemCreateProfile()
        {
            CreateMap<TodoItemCreateVm, TodoItem>();
        }
    }
}