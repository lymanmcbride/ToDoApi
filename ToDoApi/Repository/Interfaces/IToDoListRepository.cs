using ToDoApi.Models;

namespace ToDoApi.Repository.Interfaces
{
    public interface IToDoListRepository
    {
        public ToDoList GetToDoList(long id);
    }
}