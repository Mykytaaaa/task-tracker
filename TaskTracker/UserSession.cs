using TaskTracker.UI;

namespace TaskTracker
{
    public class UserSession
    {
        public const string AddTaskCommand = "add";
        public const string ListAllCommand = "list";
        public const string UpdateTaskByIdCommand = "update";
        public const string DeleteTaskByIdCommand = "delete";
        public const string FindTaskByKeywordCommand = "find";
        public const string QuitCommand = "quit";

        public const string SetDescriptionCommand = "desc: ";
        public const string SetPriorityCommand = "priority: ";
        public const string SetCategoryCommand = "category: ";
        public const string SetDueDateCommand = "due: ";

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
            ui.WriteLine($"Type \"{QuitCommand}\" to exit the application.");

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
                    else if (input.Equals(QuitCommand, StringComparison.InvariantCultureIgnoreCase))
                        break; // Quit session loop
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
            ui.WriteLine($"Use \"{SetDescriptionCommand}\" to add task description");
            ui.WriteLine($"Use \"{SetPriorityCommand}\" to add priority (number)");
            ui.WriteLine($"Use \"{SetCategoryCommand}\" to add category");
            ui.WriteLine($"Use \"{SetDueDateCommand}\" to add due date (mm.dd.yyyy)");
            ui.WriteLine($"Press Enter on an empty line to save the task to the list.");
            string? input;
            while (true)
            {
                input = ui.ReadLine();
                if (string.IsNullOrEmpty(input))
                    return taskBuilder.GetResult();
                else if (input.StartsWith(SetDescriptionCommand, true, null))
                    taskBuilder.SetDescription(input.Substring(SetDescriptionCommand.Length));
                else if (input.StartsWith(SetPriorityCommand, true, null))
                {
                    if (int.TryParse(input.Substring(SetPriorityCommand.Length), out int priority))
                        taskBuilder.SetPriority(priority);
                    else
                        ui.WriteLine("Priority must be a number.");
                }
                else if (input.StartsWith(SetCategoryCommand, true, null))
                    taskBuilder.SetCategory(input.Substring(SetCategoryCommand.Length));
                else if (input.StartsWith(SetDueDateCommand, true, null))
                {
                    if (DateOnly.TryParse(input.Substring(SetDueDateCommand.Length), out DateOnly dueDate))
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
