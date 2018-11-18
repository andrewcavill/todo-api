using System.Collections.Generic;

namespace TodoApi.ViewModels
{
    public class TodoListVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public List<TodoItemVm> Items { get; set; }
    }
}