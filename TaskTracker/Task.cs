namespace TaskTracker
{
    internal class Task
    {
        public int Id { get; private set; }
        public string Title;
        public string Description;
        public int Priority;
        public string Category;
        public DateOnly DueDate;

        public Task(int id, string title, string description, int priority, string category, DateOnly dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            Category = category;
            DueDate = dueDate;
        }
    }
}
