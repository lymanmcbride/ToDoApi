using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Middleware;
using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [ToDosExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDosController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        
        // GET: api/ToDoLists/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _toDoRepository.GetAllToDos();
        }
        
        // POST: api/ToDoLists/todos
        [HttpPost]
        public ActionResult<IEnumerable<ToDoItem>> AddToDoItem([FromBody] ToDoItem toDoItemDto)
        {
            _toDoRepository.CreateToDo(toDoItemDto);
            
            return CreatedAtAction(nameof(GetToDoItems), toDoItemDto);
        }
    }
}