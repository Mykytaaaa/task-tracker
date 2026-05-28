using TaskTracker.UI;

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
        private IUserInterface ui;

        public UserSession(TaskManager taskManager, IUserInterface ui)
        {
            this.taskManager = taskManager;
            this.ui = ui;
        }

        public void HandleUser()
        {
            ui.WriteLine($"You can add tasks by typing \"{AddTaskCommand}\" command, view them by \"{ListAllCommand}\" command, update by \"{UpdateTaskByIdCommand}\" command, delete by \"{DeleteTaskByIdCommand}\" command, and find by keyword using \"{FindTaskByKeywordCommand}\" command.");

            string? input;
            while (true)
            {
                input = ui.ReadLine();
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
                        ui.WriteLine("Unknown command");
                }
            }
        }

        private void AddTask()
        {
            string? input;
            taskBuilder.Reset();

            ui.WriteLine("Enter a title for the new task:");
            while (true)
            {
                input = ui.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    taskBuilder.SetTitle(input);
                    break;
                }
                else
                    ui.WriteLine("Please enter a title");
            }

            ui.WriteLine("Would you like to add any details?");
            var task = TaskBuilderDialog();
            taskManager.AddTask(task);
            ui.WriteLine("Task added.");
        }

        private Task TaskBuilderDialog()
        {
            ui.WriteLine($"Use \"{DescriptionCommand}\" to add task description");
            ui.WriteLine($"Use \"{PriorityCommand}\" to add priority (number)");
            ui.WriteLine($"Use \"{CategoryCommand}\" to add category");
            ui.WriteLine($"Use \"{DueDateCommand}\" to add due date (mm.dd.yyyy)");
            ui.WriteLine($"Press Enter on an empty line to save the task to the list.");
            string? input;
            while (true)
            {
                input = ui.ReadLine();
                if (string.IsNullOrEmpty(input))
                    return taskBuilder.GetResult();
                else if (input.StartsWith(DescriptionCommand, true, null))
                    taskBuilder.SetDescription(input.Substring(DescriptionCommand.Length));
                else if (input.StartsWith(PriorityCommand, true, null))
                {
                    if (int.TryParse(input.Substring(PriorityCommand.Length), out int priority))
                        taskBuilder.SetPriority(priority);
                    else
                        ui.WriteLine("Priority must be a number.");
                }
                else if (input.StartsWith(CategoryCommand, true, null))
                    taskBuilder.SetCategory(input.Substring(CategoryCommand.Length));
                else if (input.StartsWith(DueDateCommand, true, null))
                {
                    if (DateOnly.TryParse(input.Substring(DueDateCommand.Length), out DateOnly dueDate))
                        taskBuilder.SetDueDate(dueDate);
                    else
                        ui.WriteLine("Due date not recognized.");
                }
                else
                    ui.WriteLine("Unknown command");
            }
        }

        private void ListAll()
        {
            var tasks = taskManager.GetTasks();
            foreach (var task in tasks)
                ui.WriteLine(task.ToText());
        }

        private void UpdateTaskById()
        {
            string? input;

            ui.WriteLine("Enter the task id:");
            while (true)
            {
                input = ui.ReadLine();
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
                            ui.WriteLine("Task not found.");
                    }
                    else
                        ui.WriteLine("Task id must be a number.");
                }
                else
                    break;
            }

            TaskBuilderDialog();
            ui.WriteLine("Task updated.");
        }

        private void DeleteTaskById()
        {
            string? input;

            ui.WriteLine("Enter the task id:");
            input = ui.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out int taskId))
                {
                    bool deleted = taskManager.DeleteTask(taskId);
                    if (deleted)
                        ui.WriteLine("Task deleted.");
                    else
                        ui.WriteLine("Task not found.");
                }
                else
                    ui.WriteLine("Task id must be a number.");
            }
        }

        private void FindTaskByKeyword()
        {
            string? input;

            ui.WriteLine("Enter the search keyword:");
            input = ui.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                var foundTasks = taskManager.FindByKeyword(input);
                if (foundTasks == null || foundTasks.Count == 0)
                    ui.WriteLine("No tasks found.");
                else
                {
                    foreach (var task in foundTasks)
                        ui.WriteLine(task.ToText());
                }
            }
        }
    }
}
