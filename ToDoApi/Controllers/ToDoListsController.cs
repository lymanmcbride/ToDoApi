using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Middleware;
using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [ToDoListsExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IToDoRepository _toDoRepository;
        private readonly IToDoService _toDoService;

        public ToDoListsController(IToDoListRepository toDoListRepository, IToDoRepository toDoRepository, IToDoService toDoService)
        {
            _toDoListRepository = toDoListRepository;
            _toDoRepository = toDoRepository;
            _toDoService = toDoService;
        }
        
        // GET: api/ToDoLists
        [HttpGet]
        public ActionResult<IEnumerable<ToDoList>> GetToDoLists()
        {
            return _toDoListRepository.GetAllLists();
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id:long}")]
        public ActionResult<ToDoList> GetTodoList(long id)
        {
            return _toDoListRepository.GetList(id);
        }

        // POST: api/ToDoLists
        [HttpPost]
        public ActionResult<ToDoList> AddToDoList([FromBody] ToDoList toDoList)
        {
            _toDoListRepository.CreateList(toDoList);
            
            return CreatedAtAction(nameof(GetTodoList), new { id = toDoList.Id }, toDoList);
        }

        // GET: api/ToDoLists/todos
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _toDoRepository.GetAllToDos();
        }
        
        // GET: api/ToDoLists/{id}/todos
        [HttpGet("{id}/todos")]
        public ActionResult<IEnumerable<ToDoItem>> GetToDoItems(int id)
        {
            return _toDoListRepository.GetList(id).ToDoItems;
        }
        // POST: api/ToDoLists/{id}/todos
        [HttpPost("{listId}/todos")]
        public ActionResult<IEnumerable<ToDoItem>> AddToDoItemToList(long listId, [FromBody] ToDoItem toDoItemDto)
        {
            ToDoList associatedList = _toDoService.AddToDoToList(listId, toDoItemDto);

            return CreatedAtAction(nameof(GetTodoList), new { id = associatedList.Id }, associatedList);
        }
        
        // POST: api/ToDoLists/todos
        [HttpPost("todos")]
        public ActionResult<IEnumerable<ToDoItem>> AddToDoItem([FromBody] ToDoItem toDoItemDto)
        {
            _toDoRepository.CreateToDo(toDoItemDto);
            
            return CreatedAtAction(nameof(GetToDoItems), toDoItemDto);
        }
        
        // PUT: api/ToDoLists/todos/{id}/name
        [HttpPut("todos/{id}/name")]
        public ActionResult<IEnumerable<ToDoItem>> EditToDoLabel(long id, [FromBody] ToDoItem toDoItemDto)
        {
            if (id != toDoItemDto.Id)
            {
                throw new Exception("Ids must match");
            }
            ToDoItem toDoItem = _toDoService.EditToDoLabel(toDoItemDto);

            return Ok(new {message = "To Do Updated", content = toDoItem});
        }
        
        // PUT: api/ToDoLists/todos/{id}/iscomplete
        [HttpPut("todos/{id}/iscomplete")]
        public ActionResult<IEnumerable<ToDoItem>> EditToggle(long id, [FromBody] ToDoItem toDoItemDto)
        {
            if (id != toDoItemDto.Id)
            {
                throw new Exception("Ids must match");
            }
            ToDoItem toDoItem = _toDoService.EditToDoToggle(toDoItemDto);

            return Ok(new {message = "To Do Updated", content = toDoItem});
        }
        
        // DELETE: api/ToDoLists/todos/{id}
        [HttpDelete("todos/{id}")]
        public ActionResult<IEnumerable<ToDoItem>> DeleteToggle(long id)
        {
            ToDoItem toDoItem = _toDoService.DeleteToDoItemBasedOnId(id);

            return Ok(new {message = "To Do removed", content = toDoItem});
        }
    }
}
