namespace TaskTracker
{
    public class TaskBuilder
    {
        private Task task;
        private int nextTaskId = 0;

        /*public TaskBuilder()
        {
            Reset();
        }*/

        public TaskBuilder Reset()
        {
            task = new Task(nextTaskId++);
            return this;
        }

        public TaskBuilder Modify(Task newTask)
        {
            task = newTask;
            return this;
        }

        public TaskBuilder SetTitle(string title)
        {
            task.Title = title;
            return this;
        }

        public TaskBuilder SetDescription(string description)
        {
            task.Description = description;
            return this;
        }

        public TaskBuilder SetPriority(int priority)
        {
            task.Priority = priority;
            return this;
        }

        public TaskBuilder SetCategory(string category)
        {
            task.Category = category;
            return this;
        }

        public TaskBuilder SetDueDate(DateOnly dueDate)
        {
            task.DueDate = dueDate;
            return this;
        }

        public Task GetResult()
        {
            return task;
        }
    }
}
