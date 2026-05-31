using System.Text.Json;

namespace TaskTracker.FileSavers
{
    internal class JsonFileSaver : IFileSaver
    {
        private const string filename = "tasks.json";

        public void Save(string path, List<Task> tasks)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (FileStream stream = new FileStream(path + filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Utf8JsonWriter writer = new(stream);
                JsonSerializer.Serialize(writer, tasks);
            }
        }

        public List<Task>? Load(string path)
        {
            if (!Directory.Exists(path) || !File.Exists(path + filename))
                return null;

            using (FileStream stream = new FileStream(path + filename, FileMode.Open, FileAccess.Read))
            {
                return JsonSerializer.Deserialize<List<Task>>(stream);
            }
        }
    }
}
