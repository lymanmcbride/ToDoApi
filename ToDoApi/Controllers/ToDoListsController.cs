using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListsController(TodoContext context, IToDoListRepository toDoListRepository)
        {
            _context = context;
            _toDoListRepository = toDoListRepository;
            // TestData.TestData.AddTestData(_context);
        }
        // GET: api/ToDoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoLists()
        {
            return await _context.TodoLists.Include(list => list.ToDoItems).ToArrayAsync();
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id}")]
        public ActionResult<ToDoList> GetTodoList(long id)
        {
            return _toDoListRepository.GetToDoList(id);
        }

        // POST: api/ToDoLists
        [HttpPost]
        public async Task<ActionResult<ToDoList>> Post([FromBody] ToDoList toDoList)
        {
            _context.TodoLists.Add(toDoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoList), new { id = toDoList.Id }, toDoList);
        }

        // PUT: api/ToDoLists/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ToDoLists/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        // GET: api/ToDoLists/todos
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToArrayAsync();
        }
        
        // GET: api/ToDoLists/{id}/todos
        [HttpGet("{id}/todos")]
        public ActionResult<IEnumerable<ToDoItem>> GetToDoItems(int id)
        {
            var list = _toDoListRepository.GetToDoList(id);

            return list.ToDoItems;
        }
        // POST: api/ToDoLists/{id}/todos
        [HttpPost("{listId}/todos")]
        public ActionResult<IEnumerable<ToDoItem>> AddToDoItem(long listId, [FromBody] ToDoItem toDoItem)
        {
            var list = _toDoListRepository.GetToDoList(listId);
            list.ToDoItems.Add(toDoItem);
            _context.Entry(list).State = EntityState.Modified;
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoList), new { id = list.Id }, list);
        }
        
        // POST: api/ToDoLists/todos
        [HttpPost("todos")]
        public ActionResult<IEnumerable<ToDoItem>> AddToDoItem([FromBody] ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetToDoItems), toDoItem);
        }
        
        // POST: api/ToDoLists/todos/{id}
        [HttpPut("todos/{id}/name")]
        public ActionResult<IEnumerable<ToDoItem>> EditToDoLabel(long id, [FromBody] ToDoItem toDoItemDto)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            toDoItem.Name = toDoItemDto.Name;
            _context.SaveChanges();

            return Ok(new {message = "To Do Updated", content = toDoItem});
        }
        
        // POST: api/ToDoLists/todos/{id}
        [HttpPut("todos/{id}/iscomplete")]
        public ActionResult<IEnumerable<ToDoItem>> EditToggle(long id, [FromBody] ToDoItem toDoItemDto)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            toDoItem.IsComplete = toDoItemDto.IsComplete;
            _context.SaveChanges();

            return Ok(new {message = "To Do Updated", content = toDoItem});
        }
        
        // POST: api/ToDoLists/todos/{id}
        [HttpDelete("todos/{id}")]
        public ActionResult<IEnumerable<ToDoItem>> DeleteToggle(long id)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            _context.ToDoItems.Remove(toDoItem);
            _context.SaveChanges();

            return Ok(new {message = "To Do removed", content = toDoItem});
        }
    }
}
