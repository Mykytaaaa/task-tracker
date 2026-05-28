namespace TaskTracker.UI
{
    internal class ConsoleUI : IUserInterface
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
