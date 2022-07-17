using System;
using ToDoApi.Models;

namespace ToDoApi.Repository.Exceptions
{
    public class ToDoDatabaseAccessException : Exception
    {
        public ToDoDatabaseAccessException(string message, ToDoItem toDoItem) : base(CreateMessage(message, toDoItem))
        {
        }

        private static string CreateMessage(string message, ToDoItem toDoItem)
        {
            return $"{message} \nTo Do Item Name: {toDoItem.Name}, \nTo Do Item Id: {toDoItem.Id}";
        }
    }
}