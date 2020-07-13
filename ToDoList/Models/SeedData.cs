using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Data;
using System;
using System.Linq;

namespace ToDoList.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ToDoListContext(
                serviceProvider.GetRequiredService<DbContextOptions<ToDoListContext>>()))
            {
                // Look for any movies.
                if (context.SomeTask.Any())
                {
                    Console.WriteLine("DB already has records");
                    return;   // DB has been seeded
                }

                context.SomeTask.AddRange(
                    new SomeTask
                    {
                        Id = 1,
                        Title = "Clean Room",
                        Description = "Put stuff away. Clean dust. Sweep floor.",
                        Status = false
                    },

                    new SomeTask
                    {
                        Id = 2,
                        Title = "Make App",
                        Description = "Figure out how to make the app. Why is C# confusing you?",
                        Status = false
                    },

                    new SomeTask
                    {
                        Id = 3,
                        Title = "Make Dinner",
                        Description = "Get required groceries. Clean dishes. Make dinner. Clean dishes again.",
                        Status = true
                    },

                    new SomeTask
                    {
                        Id = 4,
                        Title = "Be Lazy",
                        Description = "Accomplish nothing. Procrastinate responsibilities.",
                        Status = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}