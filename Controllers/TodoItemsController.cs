using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.IServices;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [Route("api/users/{userId}/todolists/{todoListId}/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoListService todoListService, ITodoItemService todoItemService, IMapper mapper)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TodoItemVm>> GetAll(int userId, int todoListId)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            return _mapper.Map<List<TodoItemVm>>(todoList.TodoItems);
        }

        [HttpGet("{todoItemId}", Name = "GetTodoItem")]
        public ActionResult<TodoItemVm> GetById(int userId, int todoListId, int todoItemId)
        {
            var todoItem = _todoItemService.Get(userId, todoListId, todoItemId);
            if (todoItem == null) return NotFound();

            return _mapper.Map<TodoItemVm>(todoItem);
        }

        [HttpPost]
        public IActionResult Create(int userId, int todoListId, TodoItemCreateVm todoItemCreateVm)
        {
            var todoList = _todoListService.Get(userId, todoListId);
            if (todoList == null) return NotFound();

            var todoItem = _mapper.Map<TodoItem>(todoItemCreateVm);
            todoItem.TodoList = todoList;
            _todoItemService.Create(todoItem);

            return CreatedAtRoute(
                "GetTodoItem", 
                new { userId = todoList.User.Id, todoListId = todoList.Id, todoItemId = todoItem.Id }, 
                todoItem.Id);
        }

        [HttpPut("{todoItemId}/iscomplete")]
        public IActionResult UpdateIsComplete(int userId, int todoListId, int todoItemId, 
            [FromBody] bool isComplete)
        {
            var todoItem = _todoItemService.Get(userId, todoListId, todoItemId);
            if (todoItem == null) return NotFound();

            todoItem.IsComplete = isComplete;
            _todoItemService.Update(todoItem);

            return NoContent();
        }

        [HttpPut("{todoItemId}/name")]
        public IActionResult UpdateName(int userId, int todoListId, int todoItemId,
            [FromBody] string name)
        {
            var todoItem = _todoItemService.Get(userId, todoListId, todoItemId);
            if (todoItem == null) return NotFound();

            todoItem.Name = name;
            _todoItemService.Update(todoItem);

            return NoContent();
        }

        [HttpPut("{todoItemId}/isdeleted")]
        public IActionResult UpdateIsDeleted(int userId, int todoListId, int todoItemId,
            [FromBody] bool isDeleted)
        {
            var todoItem = _todoItemService.Get(userId, todoListId, todoItemId);
            if (todoItem == null) return NotFound();

            todoItem.IsDeleted = isDeleted;
            _todoItemService.Update(todoItem);

            return NoContent();
        }
    }
}