using System.Collections.Generic;
using ToDoApi.Models;

namespace ToDoApi.TestData
{
    public static class TestData
    {
        public static void AddTestData(TodoContext context)
        {
            var workToDos = new ToDoList()
            {
                Name = "Work",
                ToDoItems = new List<ToDoItem>()
                {
                    new()
                    {
                        Name = "Finish quativa coding challenge",
                        IsComplete = false
                    },
                    new()
                    {
                        Name = "Finish Clean Architecture",
                        IsComplete = false,
                    },
                    new()
                    {
                        Name = "Create Readme for new repo",
                        IsComplete = true
                    }
                }
            };
            
            var personalToDos = new ToDoList()
            {
                Name = "Personal",
                ToDoItems = new List<ToDoItem>()
                {
                    new()
                    {
                        Name = "Purchase Tooth Brush",
                        IsComplete = false
                    },
                    new()
                    {
                        Name = "Take Bike to Shop",
                        IsComplete = false,
                    },
                    new()
                    {
                        Name = "Get Snacks for desk",
                        IsComplete = true
                    }
                }
            };
            
            var churchToDos = new ToDoList()
            {
                Name = "Church",
                ToDoItems = new List<ToDoItem>()
                {
                    new()
                    {
                        Name = "Transfer Records To new Congregation",
                        IsComplete = true
                    },
                    new()
                    {
                        Name = "Talk to Leader",
                        IsComplete = false,
                    }
                }
            };
            
            var familyToDos = new ToDoList()
            {
                Name = "Family",
                ToDoItems = new List<ToDoItem>()
                {
                    new()
                    {
                        Name = "Do Family Home Evening",
                        IsComplete = false
                    },
                    new()
                    {
                        Name = "Cook Dinner",
                        IsComplete = false,
                    },
                    new()
                    {
                        Name = "Play a game with kids",
                        IsComplete = true
                    }
                }
            };
 
            context.TodoLists.Add(workToDos);
            context.TodoLists.Add(personalToDos);
            context.TodoLists.Add(churchToDos);
            context.TodoLists.Add(familyToDos);
            
            context.SaveChanges();


        }
    }
}