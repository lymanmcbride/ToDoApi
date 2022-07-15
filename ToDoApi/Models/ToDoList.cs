using System.Collections.Generic;

namespace ToDoApi.Models
{
    public class ToDoList
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }

        public ToDoList()
        {
            ToDoItems = new List<ToDoItem>();
        }
    }
}