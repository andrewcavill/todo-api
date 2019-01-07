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

        public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

        public int NumberOfItems =>  TodoItems.Count;

        public int NumberIfItemsCompleted => TodoItems.Select(x => x.IsComplete).ToList().Count;
    }
}