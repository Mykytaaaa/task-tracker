namespace TaskTracker
{
    public class Task
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public string? Category { get; set; }
        public DateOnly? DueDate { get; set; }

        private const string defaultTitle = "Untitled";

        public Task(int id)
        {
            Id = id;
            Title = defaultTitle;
        }

        public string ToText()
        {
            string result = $"Id = {Id}"
                + $"\nTitle: {Title}";

            if (!string.IsNullOrEmpty(Description))
                result += "Description: " + Description;
            if (Priority != null)
                result += "Priority: " + Priority;
            if (!string.IsNullOrEmpty(Category))
                result += "Category: " + Category;
            if (DueDate != null)
                result += "Due: " + DueDate;

            return result;
        }
    }
}
