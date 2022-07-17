using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Repository.Interfaces
{
    public interface IToDoListRepository
    {
        public ToDoList GetList(long id);
        public ToDoList[] GetAllLists();
        public void CreateList(ToDoList toDoList);
        public void SaveList(ToDoList toDoList);
        public List<ToDoItem> GetToDosAssociatedWithLists();

    }
}