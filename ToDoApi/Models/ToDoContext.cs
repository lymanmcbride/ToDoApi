using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoList> TodoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

    }
}