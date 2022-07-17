using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Repository.Exceptions;
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
        
        public List<ToDoItem> GetToDosAssociatedWithLists()
        {
            List<ToDoItem> toDoItems = new List<ToDoItem>();
            var lists = GetAllLists();
            foreach (ToDoList toDoList in lists)
            {
                toDoItems.AddRange(toDoList.ToDoItems);
            }

            return toDoItems;
        }

        public ToDoList[] GetAllLists()
        {
            return _context.TodoLists.Include(list => list.ToDoItems).ToArray();
        }

        public async void CreateList(ToDoList toDoList)
        {
            _context.TodoLists.Add(toDoList);
            var numberOfEntriesSaved = await _context.SaveChangesAsync();
            VerifySuccessfulSave(numberOfEntriesSaved);
        }

        public void SaveList(ToDoList toDoList)
        {
            _context.Entry(toDoList).State = EntityState.Modified;
            int numberOfEntriesSaved = _context.SaveChanges();
            VerifySuccessfulSave(numberOfEntriesSaved);
        }
        
        private static void VerifySuccessfulSave(int numberOfEntriesSaved)
        {
            if (numberOfEntriesSaved < 1)
            {
                throw new ListDatabaseAccessException("Could not save entry.");
            }
        }
    }
}