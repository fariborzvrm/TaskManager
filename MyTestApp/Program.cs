using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyTestApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;


public class Program

{
    
    static void ShowMenu()
    {
        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. View Tasks");
        Console.WriteLine("3. Delete Task");
        Console.WriteLine("4. Mark Task Done");
        Console.WriteLine("5. View Pending Tasks");
        Console.WriteLine("6. View Completed Tasks");
        Console.WriteLine("7. Search Tasks");
        Console.WriteLine("8. Exit");
    }
   

    static void PrintTasks(List<TaskItem> tasks)
    {
        if (!tasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Id} - {task.Title} - {(task.IsDone ? "Done" : "Pending")} - {task.CreatedAt}");
        }
    }

    public static async Task Main()
    {



        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDB;Trusted_Connection=True;")
            .Options;

        var context = new AppDbContext(options);

        var repository = new TaskRepository(context);

        var service = new TaskService(repository);







        while (true)
        {


            ShowMenu();



            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid Input!!!");
                return;
            }

            if (choice == 1)
            {
                Console.Write("Please enter your task: ");
                string title = Console.ReadLine() ?? " ";
               await service.AddTaskAsync(title);
            }

            else if (choice == 2)
            {
                 PrintTasks(await service.GetTasksAsync());

                }

            else if (choice == 3)
            {
                Console.WriteLine("Please Select id of the task you want delete!");

                PrintTasks(await service.GetTasksAsync());

                if (!int.TryParse(Console.ReadLine(),out int idToDelete))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                bool result = await service.DeleteTaskAsync(idToDelete);
                if (!result)
                {
                    Console.WriteLine("Invalid id!");
                }
                 
            }

            else if (choice == 4)
            {
                Console.WriteLine("Please Select id of the task you want to mark as done!");

                PrintTasks(await service.GetTasksAsync());

                if (!int.TryParse(Console.ReadLine(),out int id))
                {
                    Console.WriteLine("Invalid Input!");
                    return;
                }
                bool result = await service.MarkAsDoneAsync(id);
                if (!result)
                {
                    Console.WriteLine("Invalid id!");
                }
                else
                {
                    Console.WriteLine("Your task marked as done!");
                }

            }
            else if (choice == 5)
            {
                Console.WriteLine("Your pending tasks are:");
                PrintTasks(await service.GetPendingTasksAsync());

            }
            else if (choice == 6)
            {
                Console.WriteLine("Your completed tasks are:");                
                PrintTasks(await service.GetCompletedTasksAsync());
                
            }
            else if (choice == 7)
            {
                Console.WriteLine("Please enter your task title");
                string title = Console.ReadLine() ??"";
                var result = await service.SearchTasksAsync(title);
                PrintTasks(result);
                
            }


            else if (choice == 8) break;
        }

    }
}