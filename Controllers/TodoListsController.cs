using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.IServices;
using TodoApi.Models;
using TodoApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;

        public TodoListsController(IUserService userService, ITodoListService todoListService,
            IMapper mapper)
        {
            _userService = userService;
            _todoListService = todoListService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TodoListVm>> GetAll(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null) return NotFound();

            var todoLists = _todoListService.GetAll(userId);

            return _mapper.Map<List<TodoListVm>>(todoLists);
        }

        [HttpGet("{todoListId}", Name = "GetTodoList")]
        public ActionResult<TodoListVm> GetById(int userId, int todoListId)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();

            return _mapper.Map<TodoListVm>(todoList);
        }

        [HttpPost]
        public IActionResult Create(int userId, TodoListCreateVm todoListCreateVm)
        {
            var user = _userService.GetById(userId);
            if (user == null) return NotFound();

            var todoList = _mapper.Map<TodoList>(todoListCreateVm);
            todoList.User = user;
            _todoListService.Create(todoList);

            return CreatedAtRoute(
                "GetTodoList", 
                new { userId = user.Id, todoListId = todoList.Id }, 
                todoList.Id);
        }

        [HttpPut("{todoListId}")]
        public IActionResult UpdateTodoList(int userId, int todoListId, 
            TodoListUpdateVm todoListUpdateVm)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();

            _mapper.Map(todoListUpdateVm, todoList);
            _todoListService.Update(todoList);

            return NoContent();
        }

        [HttpDelete("{todoListId}")]
        public IActionResult DeleteTodoList(int userId, int todoListId)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();

            _todoListService.Delete(todoList);
            
            return NoContent();
        }
    }
}