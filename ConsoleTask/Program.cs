using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager manager = new TaskManager();
            manager.LoadFromFile();

            bool running = true;

            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n===== TASK MANAGER =====");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Show tasks");
                Console.WriteLine("3. Complete task");
                Console.WriteLine("4. Delete task");
                Console.WriteLine("5. Exit");
                Console.WriteLine("6. Sort by Name");
                Console.WriteLine("7. Sort by Status");
                Console.ResetColor();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task name: ");
                        string name = Console.ReadLine();
                        manager.AddTask(name);
                        Console.WriteLine("Task added!");
                        break;

                    case "2":
                        var tasks = manager.GetAllTasks();

                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks available.");
                        }
                        else
                        {
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                if (tasks[i].IsCompleted)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Red;

                                Console.WriteLine($"{i}. {tasks[i].Name} - {(tasks[i].IsCompleted ? "Completed" : "Not Completed")}");
                                Console.ResetColor();
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Enter task index to complete: ");
                        if (int.TryParse(Console.ReadLine(), out int completeIndex))
                        {
                            if (manager.CompleteTask(completeIndex))
                                Console.WriteLine("Task completed!");
                            else
                                Console.WriteLine("Invalid index!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter task index to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteIndex))
                        {
                            if (manager.DeleteTask(deleteIndex))
                                Console.WriteLine("Task deleted!");
                            else
                                Console.WriteLine("Invalid index!");
                        }
                        break;

                    case "5":
                        running = false;
                        break;

                    case "6":
                        manager.SortName();
                        Console.WriteLine("Tasks sorted by name.");
                        break;

                    case "7":
                        manager.SortStatus();
                        Console.WriteLine("Tasks sorted by status.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}
