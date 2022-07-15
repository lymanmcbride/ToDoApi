using ToDoApi.Models;
using ToDoApi.Repository.Interfaces;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoService(IToDoRepository toDoRepository, IToDoListRepository toDoListRepository)
        {
            _toDoRepository = toDoRepository;
            _toDoListRepository = toDoListRepository;
        }

        public ToDoList AddToDoToList(long listId, ToDoItem toDoItemDto)
        {
            var associatedList = _toDoListRepository.GetList(listId);
            associatedList.ToDoItems.Add(toDoItemDto);
            _toDoListRepository.SaveList(associatedList);

            return associatedList;
        }
        
        public ToDoItem EditToDoLabel(ToDoItem toDoItemDto)
        {
            ToDoItem toDoItem = _toDoRepository.GetTodo(toDoItemDto.Id);
            toDoItem.Name = toDoItemDto.Name;
            _toDoRepository.SaveToDo(toDoItem);

            return toDoItem;
        }
        
        public ToDoItem EditToDoToggle(ToDoItem toDoItemDto)
        {
            ToDoItem toDoItem = _toDoRepository.GetTodo(toDoItemDto.Id);
            toDoItem.IsComplete = toDoItemDto.IsComplete;
            _toDoRepository.SaveToDo(toDoItem);

            return toDoItem;
        }

        public ToDoItem DeleteToDoItemBasedOnId(long id)
        {
            ToDoItem toDoItem = _toDoRepository.GetTodo(id);
            _toDoRepository.DeleteToDo(toDoItem);
            
            return toDoItem;
        }
    }
}