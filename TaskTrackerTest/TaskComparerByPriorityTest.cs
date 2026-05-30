using TaskTracker;
using TaskTracker.TaskSortStrategies;

namespace TaskTrackerTest
{
    public class TaskComparerByPriorityTest
    {
        [Fact]
        public void GivenTasks_ThenSort_ReturnsSorted()
        {
            TaskManager taskManager = new TaskManager();
            TaskBuilder taskBuilder = new();

            taskBuilder.Reset();
            taskBuilder.SetPriority(2);
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            taskBuilder.SetPriority(1);
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            // No priority set; must be at the end of the list
            taskManager.AddTask(taskBuilder.GetResult());

            taskBuilder.Reset();
            taskBuilder.SetPriority(3);
            taskManager.AddTask(taskBuilder.GetResult());

            var tasks = taskManager.GetTasks();
            var sortedTasks = taskManager.SortTasks(new TaskComparerByPriority());

            Assert.Equal(tasks[1], sortedTasks[0]);
            Assert.Equal(tasks[0], sortedTasks[1]);
            Assert.Equal(tasks[3], sortedTasks[2]);
            Assert.Equal(tasks[2], sortedTasks[3]);
        }
    }
}
