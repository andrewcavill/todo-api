using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAllTodoItems()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodoItemById")]
        public ActionResult<TodoItem> GetTodoItemById(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult CreateTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodoItemById", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, TodoItem item)
        {
            var existingItem = _context.TodoItems.Find(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.IsComplete = item.IsComplete;
            existingItem.Name = item.Name;

            _context.TodoItems.Update(existingItem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}