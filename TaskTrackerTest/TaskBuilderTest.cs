using TaskTracker;
using Task = TaskTracker.Task;

namespace TaskTrackerTest
{
    public class TaskBuilderTest
    {
        [Fact]
        public void BuilderResetThenGetResult_ReturnsNewTask()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
        }

        [Fact]
        public void BuilderResetThenGetResult_Twice_ReturnsTwoTasks()
        {
            TaskBuilder taskBuilder = new();
            
            taskBuilder.Reset();
            Task task1 = taskBuilder.GetResult();
            taskBuilder.Reset();
            Task task2 = taskBuilder.GetResult();

            Assert.NotNull(task1);
            Assert.NotNull(task2);
            Assert.NotSame(task1, task2);
        }

        [Fact]
        public void BuildTaskWithTitleThenGetResult_ReturnsTaskWithTitle()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            string title = "Title1";
            taskBuilder.SetTitle(title);
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
            Assert.Equal(title, task.Title);
        }

        [Fact]
        public void BuildTaskWithDescriptionThenGetResult_ReturnsTaskWithDescription()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            string description = "Description1";
            taskBuilder.SetDescription(description);
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
            Assert.Equal(description, task.Description);
        }

        [Fact]
        public void BuildTaskWithPriorityThenGetResult_ReturnsTaskWithPriority()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            int priority = 12;
            taskBuilder.SetPriority(priority);
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
            Assert.Equal(priority, task.Priority);
        }

        [Fact]
        public void BuildTaskWithCategoryThenGetResult_ReturnsTaskWithCategory()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            string category = "home";
            taskBuilder.SetCategory(category);
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
            Assert.Equal(category, task.Category);
        }

        [Fact]
        public void BuildTaskWithDueDateThenGetResult_ReturnsTaskWithDueDate()
        {
            TaskBuilder taskBuilder = new();
            taskBuilder.Reset();
            DateOnly dueDate = DateOnly.Parse("02.25.2026");
            taskBuilder.SetDueDate(dueDate);
            Task task = taskBuilder.GetResult();
            Assert.NotNull(task);
            Assert.Equal(dueDate, task.DueDate);
        }

        [Fact]
        public void BuilderNotReset_ThenGetResult_ThrowsException()
        {
            TaskBuilder taskBuilder = new();
            Assert.Throws<BuilderNotResetException>(taskBuilder.GetResult);
        }
    }
}