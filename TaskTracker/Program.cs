using TaskTracker.UI;

namespace TaskTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            IUserInterface ui = new ConsoleUI();

            UserSession userSession = new UserSession(taskManager, ui);
            userSession.HandleUser();
        }
    }
}
