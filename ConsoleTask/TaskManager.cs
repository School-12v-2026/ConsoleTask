using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleTask
{
    internal class TaskManager
    {
        private List<Task> tasks;
        private string filePath;

        public TaskManager(string username)
        {
            tasks = new List<Task>();
            filePath = $"tasks_{username}.txt";
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        public void AddTask(string name)
        {
            tasks.Add(new Task(name));
            SaveToFile();
        }

        public bool CompleteTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return false;

            tasks[index].IsCompleted = true;
            SaveToFile();
            return true;
        }

        public bool DeleteTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return false;

            tasks.RemoveAt(index);
            SaveToFile();
            return true;
        }

        public void SortByName()
        {
            tasks = tasks.OrderBy(t => t.Name).ToList();
            SaveToFile();
        }

        public void SortByStatus()
        {
            tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
            SaveToFile();
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Name}|{task.IsCompleted}");
                }
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');

                tasks.Add(new Task(parts[0])
                {
                    IsCompleted = bool.Parse(parts[1])
                });
            }
        }
    }
}
