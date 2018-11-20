using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TodoListVm>> GetAllTodoLists(int userId)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();
            
            if (user == null) return NotFound();

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
        public ActionResult<TodoListVm> GetTodoList(int userId, int todoListId)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();
            
            if (user == null) return NotFound();

            var todoList = user.TodoLists.FirstOrDefault(x => x.Id == todoListId);

            if (todoList == null) return NotFound();
            
            return new TodoListVm {
                Id = todoList.Id,
                Name = todoList.Name,
                IsComplete = todoList.IsComplete
            };
        }

        [HttpPost]
        public IActionResult CreateTodoList(int userId, TodoListCreateVm todoListCreateVm)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();

            if (user == null) return NotFound();

            int nextId = _context.TodoLists.Max(x => (int)x.Id) + 1;

            var todoList = new TodoList {
                Id = nextId,
                Name = todoListCreateVm.Name,
                User = user
            };

            _context.TodoLists.Add(todoList);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetTodoList", 
                new { userId = user.Id, todoListId = todoList.Id }, 
                todoList.Id);
        }

        [HttpPut("{todoListId}")]
        public IActionResult UpdateTodoList(int userId, int todoListId, 
            TodoListUpdateVm todoListUpdateVm)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();

            if (user == null) return NotFound();

            var todoList = user.TodoLists.FirstOrDefault(x => x.Id == todoListId);

            if (todoList == null) return NotFound();

            todoList.Name = todoListUpdateVm.Name;
            todoList.IsComplete = todoListUpdateVm.IsComplete;

            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{todoListId}")]
        public IActionResult DeleteTodoList(int userId, int todoListId)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();

            if (user == null) return NotFound();

            var todoList = user.TodoLists.FirstOrDefault(x => x.Id == todoListId);

            if (todoList == null) return NotFound();

            _context.TodoLists.Remove(todoList);
            _context.SaveChanges();

            return NoContent();
        }
    }
}