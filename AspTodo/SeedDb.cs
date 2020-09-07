using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspTodo.Data;
using AspTodo.Models;
using System;
using System.Linq;

namespace AspTodo
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Todos.
                if (context.TodoItem.Any())
                {
                    return;   // DB has been seeded
                }

                context.TodoItem.AddRange(
                    new TodoItem
                    {
                        Id = new Guid(),
                        Descripton = "Vacuum whole apartment, from seed",
                        IsComplete = false,
                    },
                    new TodoItem
                    {
                        Id = new Guid(),
                        Descripton = "Finish project, from seed",
                        IsComplete = true,
                    },
                    new TodoItem
                    {
                        Id = new Guid(),
                        Descripton = "Walk the dog, from seed",
                        IsComplete = false,
                    },
                    new TodoItem
                    {
                        Id = new Guid(),
                        Descripton = "Feed baby, from seed",
                        IsComplete = false,
                    },
                    new TodoItem
                    {
                        Id = new Guid(),
                        Descripton = "Get a better job, from seed",
                        IsComplete = true,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
