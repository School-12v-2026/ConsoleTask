using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTask
{
    internal class TaskManager
    {
        private List<Task> tasks;

        public TaskManager()
        {
            tasks = new List<Task>();
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        public void AddTask(string name)
        {
            tasks.Add(new Task(name));
        }

        public bool CompleteTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return false;

            tasks[index].IsCompleted = true;
            return true;
        }

        public bool DeleteTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return false;

            tasks.RemoveAt(index);
            return true;
        }

        public void SortByName()
        {
            tasks = tasks.OrderBy(t => t.Name).ToList();
        }

        public void SortByStatus()
        {
            tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter("tasks.txt"))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Name}|{task.IsCompleted}");
                }
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists("tasks.txt"))
            {
                var lines = File.ReadAllLines("tasks.txt");

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
}
