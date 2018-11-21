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

        public TodoListsController(IUserService userService, ITodoListService todoListService)
        {
            _userService = userService;
            _todoListService = todoListService;
        }

        [HttpGet]
        public ActionResult<List<TodoListVm>> GetAll(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null) return NotFound();

            var todoLists = _todoListService.GetAll(userId);
            var todoListVms = new List<TodoListVm>();
            foreach(var list in user.TodoLists)
            {
                todoListVms.Add(new TodoListVm{
                    Id = list.Id,
                    Name = list.Name,
                    IsComplete = list.IsComplete
                });
            }
            
            return todoListVms;
        }

        [HttpGet("{todoListId}", Name = "GetTodoList")]
        public ActionResult<TodoListVm> GetById(int userId, int todoListId)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();
            
            return new TodoListVm {
                Id = todoList.Id,
                Name = todoList.Name,
                IsComplete = todoList.IsComplete
            };
        }

        [HttpPost]
        public IActionResult Create(int userId, TodoListCreateVm todoListCreateVm)
        {
            var user = _userService.GetById(userId);
            if (user == null) return NotFound();

            var todoList = new TodoList {
                Name = todoListCreateVm.Name,
                User = user
            };

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

            todoList.Name = todoListUpdateVm.Name;
            todoList.IsComplete = todoListUpdateVm.IsComplete;
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