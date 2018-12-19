using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public User User { get; set; }

        public IList<TodoItem> TodoItems = new List<TodoItem>();

        public TodoItem TodoItem(int todoItemId) => 
            TodoItems.FirstOrDefault(x => x.Id == todoItemId);

        public int NumberOfItems =>  TodoItems.Count;

        public int NumberIfItemsCompleted => TodoItems.Select(x => x.IsComplete).ToList().Count;
    }
}