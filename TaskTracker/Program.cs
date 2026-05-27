namespace TaskTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();

            UserSession userSession = new UserSession(taskManager);
            userSession.HandleUser();
        }
    }
}
