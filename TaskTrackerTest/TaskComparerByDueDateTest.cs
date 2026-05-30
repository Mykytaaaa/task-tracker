using TaskTracker;
using TaskTracker.TaskSortStrategies;

namespace TaskTrackerTest
{
    public class TaskComparerByDueDateTest
    {
        [Fact]
        public void GivenTasks_ThenSort_ReturnsSorted()
        {
            TaskManager taskManager = new TaskManager();
            TaskBuilder taskBuilder = new();

            taskBuilder.Reset();
            taskBuilder.SetDueDate(DateOnly.Parse("02.02.2025"));
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            taskBuilder.SetDueDate(DateOnly.Parse("01.01.2025"));
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            // No due date set; must be at the end of the list
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            taskBuilder.SetDueDate(DateOnly.Parse("02.02.2026"));
            taskManager.AddTask(taskBuilder.GetResult());

            var tasks = taskManager.GetTasks();
            var sortedTasks = taskManager.SortTasks(new TaskComparerByDueDate());

            Assert.Equal(tasks[1], sortedTasks[0]);
            Assert.Equal(tasks[0], sortedTasks[1]);
            Assert.Equal(tasks[3], sortedTasks[2]);
            Assert.Equal(tasks[2], sortedTasks[3]);
        }
    }
}
