using ToDoApi.Models;

namespace ToDoApi.Services.Interfaces
{
    public interface IToDoService
    {
        public ToDoList AddToDoToList(long listId, ToDoItem toDoItemDto);
        public ToDoItem EditToDoLabel(ToDoItem toDoItemDto);
        public ToDoItem EditToDoToggle(ToDoItem toDoItemDto);
        public ToDoItem DeleteToDoItemBasedOnId(long id);

    }
}