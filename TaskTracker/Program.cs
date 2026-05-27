namespace TaskTracker
{
    internal class Program
    {
        private const string AddTaskCommand = "add";

        private const string DescriptionCommand = "desc: ";
        private const string PriorityCommand = "priority: ";
        private const string CategoryCommand = "category: ";
        private const string DueDateCommand = "due: ";

        private static TaskManager taskManager = new();
        private static TaskBuilder taskBuilder = new();

        static void Main(string[] args)
        {
            Console.WriteLine($"You can add tasks by typing \"{AddTaskCommand}\" command.");

            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Equals(AddTaskCommand, StringComparison.InvariantCultureIgnoreCase))
                        AddTask();
                    else
                        Console.WriteLine("Unknown command");
                }
            }
        }

        private static void AddTask()
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
            Console.WriteLine($"Use \"{DescriptionCommand}\" to add task description");
            Console.WriteLine($"Use \"{PriorityCommand}\" to add priority (number)");
            Console.WriteLine($"Use \"{CategoryCommand}\" to add category");
            Console.WriteLine($"Use \"{DueDateCommand}\" to add due date (mm.dd.yyyy)");
            Console.WriteLine($"Press Enter on an empty line to save the task to the list.");
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    taskManager.AddTask(taskBuilder.GetResult());
                    Console.WriteLine("Task added.");
                    break;
                }
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
    }
}
