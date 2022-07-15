using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public ToDoList GetList(long id)
        {
            var db = _context.TodoLists.Include(list => list.ToDoItems).ToArray();
            return db.FirstOrDefault(list => list.Id == id);
        }

        public ToDoList[] GetAllLists()
        {
            return _context.TodoLists.Include(list => list.ToDoItems).ToArray();
        }

        public async void CreateList(ToDoList toDoList)
        {
            _context.TodoLists.Add(toDoList);
            await _context.SaveChangesAsync();
        }

        public void SaveList(ToDoList toDoList)
        {
            _context.Entry(toDoList).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}