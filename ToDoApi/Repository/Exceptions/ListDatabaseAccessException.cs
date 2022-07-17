using System;

namespace ToDoApi.Repository.Exceptions
{
    public class ListDatabaseAccessException : Exception
    {
        public ListDatabaseAccessException(string message) : base(message)
        {
        }
    }
}