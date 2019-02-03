namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public bool IsDeleted { get; set; }

        public TodoList TodoList { get; set; }
    }
}