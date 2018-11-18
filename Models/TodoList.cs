namespace TodoApi.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public User User { get; set; }
    }
}