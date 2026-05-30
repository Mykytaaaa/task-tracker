namespace TaskTracker
{
    public class TaskBuilder
    {
        private Task? task;
        private int nextTaskId = 0;

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
            if (task == null)
                throw new BuilderNotResetException();

            task.Title = title;
            return this;
        }

        public TaskBuilder SetDescription(string description)
        {
            if (task == null)
                throw new BuilderNotResetException();

            task.Description = description;
            return this;
        }

        public TaskBuilder SetPriority(int priority)
        {
            if (task == null)
                throw new BuilderNotResetException();

            task.Priority = priority;
            return this;
        }

        public TaskBuilder SetCategory(string category)
        {
            if (task == null)
                throw new BuilderNotResetException();

            task.Category = category;
            return this;
        }

        public TaskBuilder SetDueDate(DateOnly dueDate)
        {
            if (task == null)
                throw new BuilderNotResetException();

            task.DueDate = dueDate;
            return this;
        }

        public Task GetResult()
        {
            if (task == null)
                throw new BuilderNotResetException();

            return task;
        }
    }
}
