using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;

namespace ToDoApi.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly TodoContext _context;

        public ToDoListRepository(TodoContext context)
        {
            _context = context;
        }
        
        public ToDoList GetToDoList(long id)
        {
            var db = _context.TodoLists.Include(list => list.ToDoItems).ToArray();
            return db.FirstOrDefault(list => list.Id == id);
        }
    }
}