using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet("api/users/{userId}/[controller]")]
        public ActionResult<List<TodoList>> GetAllTodoLists(int userId)
        {
            var user = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.TodoLists)
                .FirstOrDefault();
            if (user == null) return NotFound();
            return user.TodoLists.ToList();

            // return _context.TodoLists
            //     .Where(x => x.User.Id == userId)
            //     .Include(x => x.User).ToList();
        }

        [HttpGet("api/users/{userId}/[controller]/{id}", Name = "GetTodoListById")]
        public ActionResult<TodoList> GetTodoListById(int id)
        {
            var list = _context.TodoLists.Where(x => x.Id == id).Include(x => x.User).FirstOrDefault();
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        [HttpPost("api/[controller]")]
        public IActionResult CreateTodoList(TodoList list)
        {
            var user = _context.Users.Find(list.User.Id);
            list.User = user;

            _context.TodoLists.Add(list);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodoListById", new { id = list.Id }, list);
        }

        [HttpPut("api/[controller]/{id}")]
        public IActionResult UpdateTodoList(int id, TodoList list)
        {
            var existingList = _context.TodoLists.Find(id);
            if (existingList == null)
            {
                return NotFound();
            }

            existingList.IsComplete = list.IsComplete;
            existingList.Name = list.Name;

            _context.TodoLists.Update(existingList);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("api/[controller]{id}")]
        public IActionResult DeleteTodoList(int id)
        {
            var list = _context.TodoLists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(list);
            _context.SaveChanges();
            return NoContent();
        }
    }
}