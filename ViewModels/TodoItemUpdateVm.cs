using System.Collections.Generic;

namespace TodoApi.ViewModels
{
    public class TodoItemUpdateVm
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}