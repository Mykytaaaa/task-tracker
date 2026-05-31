namespace TaskTracker.FileSavers
{
    internal class CsvFileSaver
    {
        public void Save(string path, List<Task> tasks)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (StreamWriter writer = new StreamWriter(path + "tasks.csv", append: false))
            {
                writer.WriteLine("Id,Title,Description,Priority,Category,DueDate");
                foreach (Task task in tasks)
                {
                    writer.WriteLine($"{task.Id},\"{task.Title}\",\"{task.Description}\",{(task.HasPriority() ? task.Priority : null)},\"{task.Category}\",{(task.HasDueDate() ? task.DueDate : null)}");
                }
                writer.Flush();
            }
        }
    }
}
