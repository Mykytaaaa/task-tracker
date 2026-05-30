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

        [Fact]
        public void ThreeTasks_FindSecondByKeywordInDesc_ReturnsSecondTask()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetTitle($"Task {i}");
                taskBuilder.SetDescription($"Task {i} fine description");
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword($"Task 1 fine");
            Assert.Single(foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void ThreeTasks_Find1And2ByKeywordInDesc_ReturnsTasks1And2()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            string[] taskDescriptions = new string[]
            {
                "Buy apples",
                "Eat apples",
                "Do homework"
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetTitle($"Task {i}");
                taskBuilder.SetDescription(taskDescriptions[i]);
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword("apples");
            Assert.Equal(2, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void ThreeTasks_Find1And2ByKeywordInDescDifferentCase_ReturnsTasks1And2()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            string[] taskDescriptions = new string[]
            {
                "Buy Apples",
                "Eat apples",
                "Do homework"
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetTitle($"Task {i}");
                taskBuilder.SetDescription(taskDescriptions[i]);
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword("apples");
            Assert.Equal(2, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void ThreeTasks_FindInexistentByKeywordInDesc_ReturnsEmptyList()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetTitle($"Task {i}");
                taskBuilder.SetDescription($"Task {i} fine description");
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword($"apples");
            Assert.Empty(foundTasks);
        }

        [Fact]
        public void ThreeTasks_Find1And2ByKeywordInTitleDifferentCase_ReturnsTasks1And2()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            string[] taskTitles = new string[]
            {
                "Buy Apples",
                "Eat apples",
                "Do homework"
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetTitle(taskTitles[i]);
                taskBuilder.SetDescription($"Task {i}");
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword("apples");
            Assert.Equal(2, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void ThreeTasks_Find1And2ByKeywordInCategoryDifferentCase_ReturnsTasks1And2()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            string[] taskCategories = new string[]
            {
                "Buy Apples",
                "Eat apples",
                "Do homework"
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetCategory(taskCategories[i]);
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FindByKeyword("apples");
            Assert.Equal(2, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void ThreeTasksWithCategories_FilterByCategory_ReturnsTasksWithThatCategory()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            string[] taskCategories = new string[]
            {
                "Home",
                "home",
                "Cook"
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetCategory(taskCategories[i]);
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var foundTasks = taskManager.FilterByCategory("Home");
            Assert.Equal(2, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
        }

        [Fact]
        public void FiveTasksWithDueDates_FilterByDueDate_ReturnsTasksWithinDueDateRange()
        {
            TaskManager taskManager = new();
            TaskBuilder taskBuilder = new();

            List<Task> tasks = new();
            DateOnly[] taskDueDates = new DateOnly[]
            {
                DateOnly.Parse("02.01.2026"),
                DateOnly.Parse("08.02.2026"),
                DateOnly.Parse("05.02.2026"),
                DateOnly.Parse("03.01.2026"),
                DateOnly.Parse("09.02.2026")
            };
            for (int i = 0; i < 3; i++)
            {
                taskBuilder.Reset();
                taskBuilder.SetDueDate(taskDueDates[i]);
                var task = taskBuilder.GetResult();
                taskManager.AddTask(task);
                tasks.Add(task);
            }

            var dateFrom = DateOnly.Parse("02.01.2026");
            var dateTo = DateOnly.Parse("08.02.2026");
            var foundTasks = taskManager.FilterByDueDate(dateFrom, dateTo);
            Assert.Equal(3, foundTasks.Count);
            Assert.Contains(tasks[0], foundTasks);
            Assert.Contains(tasks[1], foundTasks);
            Assert.Contains(tasks[2], foundTasks);
        }
    }
}
