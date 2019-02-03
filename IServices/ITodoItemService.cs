using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.IServices
{
    public interface ITodoItemService
    {
        List<TodoItem> GetAll(int userId, int todoListId);

        TodoItem Get(int userId, int todoListId, int todoItemId);

        void Create(TodoItem todoItem);

        void Update(TodoItem todoItem);
    }
}