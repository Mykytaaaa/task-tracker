using TaskTracker;

namespace TaskTrackerTest
{
    public class TaskTest
    {
        [Fact]
        public void TaskWithTitle_ToString_ContainsTitle()
        {
            TaskBuilder builder = new();
            builder.Reset();
            string title = "Test title";
            builder.SetTitle("Test title");
            var task = builder.GetResult();
            string strTask = task.ToText();
            Assert.Contains(title, strTask);
        }

        [Fact]
        public void TaskWithDesc_ToString_ContainsDesc()
        {
            TaskBuilder builder = new();
            builder.Reset();
            string desc = "Test desc";
            builder.SetDescription("Test desc");
            var task = builder.GetResult();
            string strTask = task.ToText();
            Assert.Contains(desc, strTask);
        }

        [Fact]
        public void TaskWithPriority_ToString_ContainsPriority()
        {
            TaskBuilder builder = new();
            builder.Reset();
            int priority = 4;
            builder.SetPriority(priority);
            var task = builder.GetResult();
            string strTask = task.ToText();
            Assert.Contains(priority.ToString(), strTask);
        }

        [Fact]
        public void TaskWithCategory_ToString_ContainsCategory()
        {
            TaskBuilder builder = new();
            builder.Reset();
            string category = "Test category";
            builder.SetCategory("Test category");
            var task = builder.GetResult();
            string strTask = task.ToText();
            Assert.Contains(category, strTask);
        }

        [Fact]
        public void TaskWithDueDate_ToString_ContainsDueDate()
        {
            TaskBuilder builder = new();
            builder.Reset();
            DateOnly dueDate = DateOnly.Parse("01.28.2026");
            builder.SetDueDate(dueDate);
            var task = builder.GetResult();
            string strTask = task.ToText();
            Assert.Contains(dueDate.ToString(), strTask);
        }
    }
}
