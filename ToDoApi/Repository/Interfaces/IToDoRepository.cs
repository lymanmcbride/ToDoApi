using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Repository.Interfaces
{
    public interface IToDoRepository
    {
        public void CreateToDo(ToDoItem toDoItem);
        public ToDoItem GetTodo(long id);
        public void SaveToDo(ToDoItem toDoItem);
        public void DeleteToDo(ToDoItem toDoItem);
        public Task<ToDoItem[]> GetAllToDos();

    }
}