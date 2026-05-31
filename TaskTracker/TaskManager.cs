using System.Collections.Generic;

namespace TaskTracker
{
    public class TaskManager
    {
        private List<Task> tasks;

        public TaskManager()
        {
            tasks = new List<Task>();
        }

        public TaskManager(List<Task> tasks)
        {
            this.tasks = tasks;
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public Task? GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public bool DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                return true;
            }
            return false;
        }

        public List<Task> FindByKeyword(string keyword)
        {
            List<Task> list = new();
            foreach (var task in tasks)
            {
                if (task.Title.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || task.Description != null && task.Description.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || task.Category != null && task.Category.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    )
                    list.Add(task);
            }
            return list;
        }

        public List<Task> FilterByCategory(string category)
        {
            return tasks.FindAll(t => category.Equals(t.Category, StringComparison.OrdinalIgnoreCase));
        }

        public List<Task> FilterByDueDate(DateOnly from, DateOnly to)
        {
            return tasks.FindAll(t => t.HasDueDate() && t.DueDate >= from && t.DueDate <= to);
        }
        
        public List<Task> SortTasks(IComparer<Task> taskComparer)
        {
            var sortedTasks = new List<Task>(tasks.Count);
            foreach (var task in tasks)
                sortedTasks.Add(task);

            sortedTasks.Sort(taskComparer);
            return sortedTasks;
        }
    }
}
