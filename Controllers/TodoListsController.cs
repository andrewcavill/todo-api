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

        [HttpGet("{todoListId}", Name = "GetTodoListById")]
        public ActionResult<TodoListVm> GetTodoListById(int userId, int todoListId)
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

        // [HttpPost("api/[controller]")]
        // public IActionResult CreateTodoList(TodoList list)
        // {
        //     var user = _context.Users.Find(list.User.Id);
        //     list.User = user;

        //     _context.TodoLists.Add(list);
        //     _context.SaveChanges();

        //     return CreatedAtRoute("GetTodoListById", new { id = list.Id }, list);
        // }

        // [HttpPut("api/[controller]/{id}")]
        // public IActionResult UpdateTodoList(int id, TodoList list)
        // {
        //     var existingList = _context.TodoLists.Find(id);
        //     if (existingList == null)
        //     {
        //         return NotFound();
        //     }

        //     existingList.IsComplete = list.IsComplete;
        //     existingList.Name = list.Name;

        //     _context.TodoLists.Update(existingList);
        //     _context.SaveChanges();
            
        //     return NoContent();
        // }

        // [HttpDelete("api/[controller]{id}")]
        // public IActionResult DeleteTodoList(int id)
        // {
        //     var list = _context.TodoLists.Find(id);
        //     if (list == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.TodoLists.Remove(list);
        //     _context.SaveChanges();
        //     return NoContent();
        // }
    }
}