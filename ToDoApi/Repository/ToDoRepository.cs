using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Repository.Exceptions;
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
            int numberOfEntriesSaved = _context.SaveChanges();
            numberOfEntriesSaved = 0;
            VerifySuccessfulSave(numberOfEntriesSaved, toDoItem);
        }

        public ToDoItem GetTodo(long id)
        {
            return _context.ToDoItems.Find(id);
        }
        
        public void SaveToDo(ToDoItem toDoItem)
        {
            _context.Entry(toDoItem).State = EntityState.Modified;
            int numberOfEntriesSaved = _context.SaveChanges();
            VerifySuccessfulSave(numberOfEntriesSaved, toDoItem);
        }
        
        public void DeleteToDo(ToDoItem toDoItem)
        {
            _context.ToDoItems.Remove(toDoItem);
            int numberOfEntriesSaved = _context.SaveChanges();
            VerifySuccessfulSave(numberOfEntriesSaved, toDoItem);

        }
        
        private static void VerifySuccessfulSave(int numberOfEntriesSaved, ToDoItem toDo)
        {
            if (numberOfEntriesSaved < 1)
            {
                throw new ToDoDatabaseAccessException("Could not save entry.", toDo);
            }
        }
    }
}