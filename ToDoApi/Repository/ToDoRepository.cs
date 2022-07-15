using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;

namespace ToDoApi.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TodoContext _context;

        public ToDoRepository(TodoContext context)
        {
            _context = context;
        }
        
        public async Task<ToDoItem[]> GetAllToDos()
        {
            return await _context.ToDoItems.ToArrayAsync();
        }

        public void CreateToDo(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            _context.SaveChanges();
        }

        public ToDoItem GetTodo(long id)
        {
            return _context.ToDoItems.Find(id);
        }
        
        public void SaveToDo(ToDoItem toDoItem)
        {
            _context.Entry(toDoItem).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
        public void DeleteToDo(ToDoItem toDoItem)
        {
            _context.ToDoItems.Remove(toDoItem);
            _context.SaveChanges();
        }
    }
}