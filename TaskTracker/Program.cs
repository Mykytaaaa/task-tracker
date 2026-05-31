using TaskTracker.FileSavers;
using TaskTracker.UI;

namespace TaskTracker
{
    internal class Program
    {
        public const string relativeExportPath = "tasks-data-export\\";

        static void Main(string[] args)
        {
            string path = AppContext.BaseDirectory + relativeExportPath;

            TaskManager taskManager;
            TaskBuilder taskBuilder;
            IUserInterface ui = new ConsoleUI();

            IFileSaver fileSaver = new JsonFileSaver();
            var tasks = fileSaver.Load(path);
            if (tasks != null)
            {
                int highestId = -1;
                foreach (var task in tasks)
                    if (task.Id > highestId)
                        highestId = task.Id;

                taskBuilder = new TaskBuilder(highestId + 1);
                taskManager = new TaskManager(tasks);
            }
            else
            {
                taskBuilder = new TaskBuilder();
                taskManager = new TaskManager();
            }

            UserSession userSession = new UserSession(taskManager, taskBuilder, ui);
            userSession.HandleUser();

            fileSaver.Save(path, taskManager.GetTasks());
            ui.WriteLine("Saved tasks data in JSON to: " + path);
            ui.WriteLine("<Press Enter to close the app>");
            ui.ReadLine();
        }
    }
}
