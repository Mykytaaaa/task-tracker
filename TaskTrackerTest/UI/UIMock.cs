using TaskTracker.UI;

namespace TaskTrackerTest.UI
{
    internal class UIMock : IUserInterface
    {
        public string output = string.Empty;
        public List<string> userInput = new();
        private int nextInputId = 0;

        public void WriteLine(string line)
        {
            output += line + "\n";
        }

        public string? ReadLine()
        {
            return userInput[nextInputId++];
        }
    }
}
