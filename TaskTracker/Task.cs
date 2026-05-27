namespace TaskTracker
{
    public class Task
    {
        public int Id { get; private set; }
        public string? Title;
        public string? Description;
        public int Priority;
        public string? Category;
        public DateOnly DueDate;

        private const int defaultPriority = -1;
        private readonly DateOnly defaultDueDate = DateOnly.MaxValue;

        public Task(int id)
        {
            Id = id;
        }

        public override string ToString()
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
            return Priority != defaultPriority;
        }
    }
}
