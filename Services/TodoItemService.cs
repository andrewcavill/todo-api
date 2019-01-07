using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.IServices;

namespace TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

        public List<TodoItem> GetAll(int userId, int todoListId)
        {
            return _context.TodoItems
                .Where(x => x.TodoList.Id == todoListId && x.TodoList.User.Id == userId)
                .ToList();
        }

        public TodoItem Get(int userId, int todoListId, int todoItemId)
        {
            return _context.TodoItems
                .Where(x => x.TodoList.User.Id == userId 
                    && x.TodoList.Id == todoListId
                    && x.Id == todoItemId)
                .FirstOrDefault();
        }

        public void Create(TodoItem todoItem)
        {
            int nextId = _context.TodoItems.Any() 
                ? _context.TodoItems.Max(x => (int)x.Id) + 1
                : 1;
            todoItem.Id = nextId;

            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public void Update(TodoItem todoItem)
        {
            _context.Update(todoItem);
            _context.SaveChanges();
        }

        public void Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
        }
    }
}