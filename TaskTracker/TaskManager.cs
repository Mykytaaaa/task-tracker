namespace TaskTracker
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

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
    }
}
