using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.IServices
{
    public interface ITodoListService
    {
        List<TodoList> GetAll(int userId);

        TodoList Get(int userId, int todoListId);

        void Create(TodoList todoList);

        void Update(TodoList todoList);

        void Delete(TodoList todoList);
    }
}