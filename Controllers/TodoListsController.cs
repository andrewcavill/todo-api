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
            var user = _userService.Get(userId);
            if (user == null) return NotFound();

            var todoLists = _todoListService.GetAll(userId);

            return _mapper.Map<List<TodoListVm>>(todoLists);
        }

        [HttpGet("{todoListId}", Name = "GetTodoList")]
        public ActionResult<TodoListVm> GetById(int userId, int todoListId)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            return _mapper.Map<TodoListVm>(todoList);
        }

        [HttpPost]
        public IActionResult Create(int userId, TodoListCreateVm todoListCreateVm)
        {
            var user = _userService.Get(userId);
            if (user == null) return NotFound();

            var todoList = _mapper.Map<TodoList>(todoListCreateVm);
            todoList.User = user;
            _todoListService.Create(todoList);

            return CreatedAtRoute(
                "GetTodoList", 
                new { userId = user.Id, todoListId = todoList.Id }, 
                todoList.Id);
        }

        [HttpPut("{todoListId}/complete")]
        public IActionResult UpdateComplete(int userId, int todoListId, 
            [FromBody] bool complete)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            todoList.IsComplete = complete;
            _todoListService.Update(todoList);

            return NoContent();
        }

        [HttpPut("{todoListId}/name")]
        public IActionResult UpdateName(int userId, int todoListId, 
            [FromBody] string name)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            todoList.Name = name;
            _todoListService.Update(todoList);

            return NoContent();
        }

        [HttpDelete("{todoListId}")]
        public IActionResult Delete(int userId, int todoListId)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            _todoListService.Delete(todoList);
            
            return NoContent();
        }
    }
}