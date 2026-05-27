namespace TaskTracker
{
    internal class TaskManager
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
    }
}
