namespace TaskTracker
{
    public class Task
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public string? Category { get; set; }
        public DateOnly DueDate { get; set; }

        private const string defaultTitle = "Untitled";
        private const int defaultPriority = -1;
        private readonly DateOnly defaultDueDate = DateOnly.MaxValue;

        public Task(int id)
        {
            Id = id;
            Title = defaultTitle;
            Priority = defaultPriority;
            DueDate = defaultDueDate;
        }

        public string ToText()
        {
            string result = $"Id = {Id}"
                + $"\nTitle: {Title}";

            if (!string.IsNullOrEmpty(Description))
                result += "Description: " + Description;
            if (HasPriority())
                result += "Priority: " + Priority;
            if (!string.IsNullOrEmpty(Category))
                result += "Category: " + Category;
            if (HasDueDate())
                result += "Due: " + DueDate;

            return result;
        }

        public bool HasPriority()
        {
            return Priority != defaultPriority;
        }

        public bool HasDueDate()
        {
            return DueDate != defaultDueDate;
        }
    }
}
