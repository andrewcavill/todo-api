using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.IServices;

namespace TodoApi.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoContext _context;

        public TodoListService(TodoContext context)
        {
            _context = context;
        }

        public List<TodoList> GetAll(int userId)
        {
            return _context.TodoLists
                .Where(x => x.User.Id == userId)
                .ToList();
        }

        public TodoList GetById(int userId, int todoListId)
        {
            return _context.TodoLists
                .Where(x => x.User.Id == userId && x.Id == todoListId)
                .FirstOrDefault();
        }

        public void Create(TodoList todoList)
        {
            int nextId = _context.TodoLists.Max(x => (int)x.Id) + 1;
            todoList.Id = nextId;

            _context.TodoLists.Add(todoList);
            _context.SaveChanges();
        }

        public void Update(TodoList todoList)
        {
            _context.Update(todoList);
            _context.SaveChanges();
        }

        public void Delete(TodoList todoList)
        {
            _context.TodoLists.Remove(todoList);
            _context.SaveChanges();
        }
    }
}