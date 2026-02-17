using System;

namespace ConsoleTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            TaskManager manager = new TaskManager(username);
            manager.LoadFromFile();

            while (true)
            {
                Console.Clear();
                PrintMenu();

                int choice = ReadIntInRange("Choose option: ", 1, 7);
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        AddTask(manager);
                        break;

                    case 2:
                        ShowTasks(manager);
                        break;

                    case 3:
                        CompleteTask(manager);
                        break;

                    case 4:
                        DeleteTask(manager);
                        break;

                    case 5:
                        manager.SortByName();
                        Console.WriteLine("Sorted by name.");
                        ShowTasks(manager);
                        break;

                    case 6:
                        manager.SortByStatus();
                        Console.WriteLine("Sorted by status.");
                        ShowTasks(manager);
                        break;

                    case 7:
                        return;
                }
            }
        }

        static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===== TASK MANAGER =====");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Show tasks");
            Console.WriteLine("3. Complete task");
            Console.WriteLine("4. Delete task");
            Console.WriteLine("5. Sort by Name");
            Console.WriteLine("6. Sort by Status");
            Console.WriteLine("7. Exit");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void AddTask(TaskManager manager)
        {
            Console.Write("Enter task name: ");
            string name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                manager.AddTask(name.Trim());
                Console.WriteLine("Task added.");
            }
            else
            {
                Console.WriteLine("Task name cannot be empty.");
            }

            Pause();
        }

        static void ShowTasks(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks.");
                Pause();
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.Write($"{i + 1}. {tasks[i].Name} - ");

                if (tasks[i].IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Completed");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not Completed");
                }

                Console.ResetColor();
            }

            Pause();
        }

        static void CompleteTask(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks.");
                Pause();
                return;
            }

            ShowTasks(manager);

            int number = ReadIntInRange("Task number: ", 1, tasks.Count);
            manager.CompleteTask(number - 1);

            Console.WriteLine("Task completed.");
            Pause();
        }

        static void DeleteTask(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks.");
                Pause();
                return;
            }

            ShowTasks(manager);

            int number = ReadIntInRange("Task number to delete: ", 1, tasks.Count);
            manager.DeleteTask(number - 1);

            Console.WriteLine("Task deleted.");
            Pause();
        }

        static int ReadIntInRange(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) &&
                    value >= min && value <= max)
                    return value;

                Console.WriteLine($"Enter number between {min} and {max}.");
            }
        }

        static void Pause()
        {
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }
    }
}
