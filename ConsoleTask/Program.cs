using System;

namespace ConsoleTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager manager = new TaskManager();

            while (true)
            {
                Console.Clear();
                PrintMenu();

                int choice = ReadIntInRange("Избери опция: ", 1, 7);

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
                        Console.WriteLine("Сортирано по име.");
                        Pause();
                        break;

                    case 6:
                        manager.SortByStatus();
                        Console.WriteLine("Сортирано по статус.");
                        Pause();
                        break;

                    case 7:
                        return;
                }
            }
        }

        // ---------------- MENU ----------------

        static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("=== TASK MANAGER ===");
            Console.WriteLine("1. Добави задача");
            Console.WriteLine("2. Покажи всички задачи");
            Console.WriteLine("3. Маркирай като завършена");
            Console.WriteLine("4. Изтрий задача");
            Console.WriteLine("5. Сортирай по име");
            Console.WriteLine("6. Сортирай по статус");
            Console.WriteLine("7. Изход");

            Console.ResetColor();
            Console.WriteLine();
        }

        static void AddTask(TaskManager manager)
        {
            string name = ReadNonEmptyString("Въведи име на задача: ");
            manager.AddTask(name);

            Console.WriteLine("Задачата е добавена.");
            Pause();
        }

        static void ShowTasks(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("Няма задачи.");
                Pause();
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.Write($"{i + 1}. {tasks[i].Name} - ");

                if (tasks[i].IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Завършена");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Незавършена");
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
                Console.WriteLine("Няма задачи.");
                Pause();
                return;
            }

            ShowTasks(manager);

            int number = ReadIntInRange("Номер на задача: ", 1, tasks.Count);
            manager.CompleteTask(number - 1);

            Console.WriteLine("Задачата е маркирана.");
            Pause();
        }

        static void DeleteTask(TaskManager manager)
        {
            var tasks = manager.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("Няма задачи.");
                Pause();
                return;
            }

            ShowTasks(manager);

            int number = ReadIntInRange("Номер на задача за изтриване: ", 1, tasks.Count);
            manager.DeleteTask(number - 1);

            Console.WriteLine("Задачата е изтрита.");
            Pause();
        }

        static int ReadIntInRange(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Моля въведи число.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Въведи число между {min} и {max}.");
                    continue;
                }

                return value;
            }
        }
        static string ReadNonEmptyString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Текстът не може да е празен.");
                    continue;
                }

                return input.Trim();
            }
        }
        static void Pause()
        {
            Console.WriteLine("Натисни Enter...");
            Console.ReadLine();
        }
    }
}
