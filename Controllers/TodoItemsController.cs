using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.IServices;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [Route("api/users/{userId}/todolists/{todoListId}/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;

        public TodoItemsController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TodoItemVm>> GetAllTodoItems(int userId, int todoListId)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();

            return _mapper.Map<List<TodoItemVm>>(todoList.TodoItems);
        }

        [HttpGet("{todoItemId}", Name = "GetTodoItemById")]
        public ActionResult<TodoItemVm> GetTodoItemById(int userId, int todoListId, int todoItemId)
        {
            var todoList = _todoListService.GetById(userId, todoListId);
            if (todoList == null) return NotFound();

            var todoItem = todoList.TodoItem(todoItemId);
            if (todoItem == null) return NotFound();

            return _mapper.Map<TodoItemVm>(todoItem);
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