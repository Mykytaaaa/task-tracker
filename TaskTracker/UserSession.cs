namespace TaskTracker
{
    internal class UserSession
    {
        private const string AddTaskCommand = "add";
        private const string ListAllCommand = "list";
        private const string UpdateTaskByIdCommand = "update";
        private const string DeleteTaskByIdCommand = "delete";
        private const string FindTaskByKeywordCommand = "find";

        private const string DescriptionCommand = "desc: ";
        private const string PriorityCommand = "priority: ";
        private const string CategoryCommand = "category: ";
        private const string DueDateCommand = "due: ";

        private TaskManager taskManager;
        private TaskBuilder taskBuilder = new();

        public UserSession(TaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        public void HandleUser()
        {
            Console.WriteLine($"You can add tasks by typing \"{AddTaskCommand}\" command, view them by \"{ListAllCommand}\" command, update by \"{UpdateTaskByIdCommand}\" command, delete by \"{DeleteTaskByIdCommand}\" command, and find by keyword using \"{FindTaskByKeywordCommand}\" command.");

            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Equals(AddTaskCommand, StringComparison.InvariantCultureIgnoreCase))
                        AddTask();
                    else if (input.Equals(ListAllCommand, StringComparison.InvariantCultureIgnoreCase))
                        ListAll();
                    else if (input.Equals(UpdateTaskByIdCommand, StringComparison.InvariantCultureIgnoreCase))
                        UpdateTaskById();
                    else if (input.Equals(DeleteTaskByIdCommand, StringComparison.InvariantCultureIgnoreCase))
                        DeleteTaskById();
                    else if (input.Equals(FindTaskByKeywordCommand, StringComparison.InvariantCultureIgnoreCase))
                        FindTaskByKeyword();
                    else
                        Console.WriteLine("Unknown command");
                }
            }
        }

        private void AddTask()
        {
            string? input;
            taskBuilder.Reset();

            Console.WriteLine("Enter a title for the new task:");
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    taskBuilder.SetTitle(input);
                    break;
                }
                else
                    Console.WriteLine("Please enter a title");
            }

            Console.WriteLine("Would you like to add any details?");
            var task = TaskBuilderDialog();
            taskManager.AddTask(task);
            Console.WriteLine("Task added.");
        }

        private Task TaskBuilderDialog()
        {
            Console.WriteLine($"Use \"{DescriptionCommand}\" to add task description");
            Console.WriteLine($"Use \"{PriorityCommand}\" to add priority (number)");
            Console.WriteLine($"Use \"{CategoryCommand}\" to add category");
            Console.WriteLine($"Use \"{DueDateCommand}\" to add due date (mm.dd.yyyy)");
            Console.WriteLine($"Press Enter on an empty line to save the task to the list.");
            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    return taskBuilder.GetResult();
                else if (input.StartsWith(DescriptionCommand, true, null))
                    taskBuilder.SetDescription(input.Substring(DescriptionCommand.Length));
                else if (input.StartsWith(PriorityCommand, true, null))
                {
                    if (int.TryParse(input.Substring(PriorityCommand.Length), out int priority))
                        taskBuilder.SetPriority(priority);
                    else
                        Console.WriteLine("Priority must be a number.");
                }
                else if (input.StartsWith(CategoryCommand, true, null))
                    taskBuilder.SetCategory(input.Substring(CategoryCommand.Length));
                else if (input.StartsWith(DueDateCommand, true, null))
                {
                    if (DateOnly.TryParse(input.Substring(DueDateCommand.Length), out DateOnly dueDate))
                        taskBuilder.SetDueDate(dueDate);
                    else
                        Console.WriteLine("Due date not recognized.");
                }
                else
                    Console.WriteLine("Unknown command");
            }
        }

        private void ListAll()
        {
            var tasks = taskManager.GetTasks();
            foreach (var task in tasks)
                Console.WriteLine(task.ToText());
        }

        private void UpdateTaskById()
        {
            string? input;

            Console.WriteLine("Enter the task id:");
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (int.TryParse(input, out int taskId))
                    {
                        var task = taskManager.GetTaskById(taskId);
                        if (task != null)
                        {
                            taskBuilder.Modify(task);
                            break;
                        }
                        else
                            Console.WriteLine("Task not found.");
                    }
                    else
                        Console.WriteLine("Task id must be a number.");
                }
                else
                    break;
            }

            TaskBuilderDialog();
            Console.WriteLine("Task updated.");
        }

        private void DeleteTaskById()
        {
            string? input;

            Console.WriteLine("Enter the task id:");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out int taskId))
                {
                    bool deleted = taskManager.DeleteTask(taskId);
                    if (deleted)
                        Console.WriteLine("Task deleted.");
                    else
                        Console.WriteLine("Task not found.");
                }
                else
                    Console.WriteLine("Task id must be a number.");
            }
        }

        private void FindTaskByKeyword()
        {
            string? input;

            Console.WriteLine("Enter the search keyword:");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                var foundTasks = taskManager.FindByKeyword(input);
                if (foundTasks == null || foundTasks.Count == 0)
                    Console.WriteLine("No tasks found.");
                else
                {
                    foreach (var task in foundTasks)
                        Console.WriteLine(task.ToText());
                }
            }
        }
    }
}
