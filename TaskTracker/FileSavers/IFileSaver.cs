namespace TaskTracker.FileSavers
{
    internal interface IFileSaver
    {
        public void Save(string path, List<Task> tasks);

        public List<Task> Load(string path);
    }
}
