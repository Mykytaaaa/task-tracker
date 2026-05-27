using TaskTracker;
using Task = TaskTracker.Task;

namespace TaskTrackerTest
{
    public class TaskManagerTest
    {
        [Fact]
        public void ThreeTasks_DeleteSecond_CorrectTaskDeleted()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            bool deleted = taskManager.DeleteTask(tasks[1].Id);
            Assert.True(deleted);

            var remainingTasks = taskManager.GetTasks();
            Assert.Contains(tasks[0], remainingTasks);
            Assert.DoesNotContain(tasks[1], remainingTasks);
            Assert.Contains(tasks[2], remainingTasks);
        }

        [Fact]
        public void OneTask_DeleteInexistent_TaskNotFound()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            taskBuilder.Reset();
            var task = taskBuilder.GetResult();
            taskManager.AddTask(task);

            bool deleted = taskManager.DeleteTask(task.Id + 1);
            Assert.False(deleted);
        }
    }
}
