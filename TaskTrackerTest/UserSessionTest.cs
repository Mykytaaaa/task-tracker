using TaskTracker;
using TaskTrackerTest.UI;

namespace TaskTrackerTest
{
    public class UserSessionTest
    {
        [Fact]
        public void OpenSession_ThenQuit_SessionEnds()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();
        }

        [Fact]
        public void OpenSession_AddTaskThenList_OutputContainsTask()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Eggs");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Buy eggs");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.ListAllCommand);
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Eggs", uiMock.output);
            Assert.Contains("Buy eggs", uiMock.output);
        }

        [Fact]
        public void OpenSession_AddTaskThenUpdateThenList_OutputContainsUpdatedTask()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Eggs");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Buy eggs");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.UpdateTaskByIdCommand);
            uiMock.userInput.Add("0");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Cook eggs");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "groceries");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.ListAllCommand);
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Cook eggs", uiMock.output);
            Assert.Contains("groceries", uiMock.output);
            Assert.DoesNotContain("Buy eggs", uiMock.output);
        }

        [Fact]
        public void OpenSession_AddTaskThenDeleteThenList_OutputContainsNoTask()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Eggs");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Buy eggs");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.DeleteTaskByIdCommand);
            uiMock.userInput.Add("0");
            uiMock.userInput.Add(UserSession.ListAllCommand);
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.DoesNotContain("Eggs", uiMock.output);
            Assert.DoesNotContain("Buy eggs", uiMock.output);
        }

        [Fact]
        public void OpenSession_AddTaskThenFindByKeyword_OutputContainsTask()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Eggs");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Buy eggs");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.FindTaskByKeywordCommand);
            uiMock.userInput.Add("eggs");
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Eggs", uiMock.output);
            Assert.Contains("Buy eggs", uiMock.output);
        }

        [Fact]
        public void OpenSession_AddTaskWithAllPropertiesThenList_OutputContainsAllTaskProperties()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Eggs");
            uiMock.userInput.Add(UserSession.SetDescriptionCommand + "Buy eggs");
            uiMock.userInput.Add(UserSession.SetPriorityCommand + "2");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "groceries");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "11.11.2026");
            uiMock.userInput.Add(string.Empty);
            uiMock.userInput.Add(UserSession.ListAllCommand);
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Eggs", uiMock.output);
            Assert.Contains("Buy eggs", uiMock.output);
            Assert.Contains("2", uiMock.output);
            Assert.Contains("groceries", uiMock.output);
            Assert.True(uiMock.output.Contains("11.11.2026")
                    || uiMock.output.Contains("11/11/2026"));
        }

        [Fact]
        public void OpenSessionAddTasks_ThenFilterByCategory_OutputContainsFilteredTasks()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Buy eggs");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "Home");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Go for a walk");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "Rest");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Cook eggs");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "home");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.FilterTasksCommand);
            uiMock.userInput.Add(UserSession.SetFilteringByCategoryCommand + "home");
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Buy eggs", uiMock.output);
            Assert.Contains("Cook eggs", uiMock.output);
            Assert.DoesNotContain("Go for a walk", uiMock.output);
        }

        [Fact]
        public void OpenSessionAddTasks_ThenFilterByInexistingCategory_OutputContainsNoTasks()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Buy eggs");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "Home");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Go for a walk");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "Rest");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Cook eggs");
            uiMock.userInput.Add(UserSession.SetCategoryCommand + "home");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.FilterTasksCommand);
            uiMock.userInput.Add(UserSession.SetFilteringByCategoryCommand + "Cooking");
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.DoesNotContain("Buy eggs", uiMock.output);
            Assert.DoesNotContain("Cook eggs", uiMock.output);
            Assert.DoesNotContain("Go for a walk", uiMock.output);
        }

        [Fact]
        public void OpenSessionAddTasks_ThenFilterByDueDate_OutputContainsFilteredTasks()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Buy eggs");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "10.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Go for a walk");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "12.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Cook eggs");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "11.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.FilterTasksCommand);
            uiMock.userInput.Add(UserSession.SetFilteringByDueDateCommand + "11.01.2026-12.01.2026");
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.Contains("Go for a walk", uiMock.output);
            Assert.Contains("Cook eggs", uiMock.output);
            Assert.DoesNotContain("Buy eggs", uiMock.output);
        }

        [Fact]
        public void OpenSessionAddTasks_ThenFilterByInexistentDueDateRange_OutputContainsNoTasks()
        {
            var uiMock = new UIMock();
            var userSession = new UserSession(new TaskManager(), new TaskBuilder(), uiMock);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Buy eggs");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "10.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Go for a walk");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "12.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.AddTaskCommand);
            uiMock.userInput.Add("Cook eggs");
            uiMock.userInput.Add(UserSession.SetDueDateCommand + "11.01.2026");
            uiMock.userInput.Add(string.Empty);

            uiMock.userInput.Add(UserSession.FilterTasksCommand);
            uiMock.userInput.Add(UserSession.SetFilteringByDueDateCommand + "11.01.2027-12.01.2027");
            uiMock.userInput.Add(UserSession.QuitCommand);

            userSession.HandleUser();

            Assert.DoesNotContain("Go for a walk", uiMock.output);
            Assert.DoesNotContain("Cook eggs", uiMock.output);
            Assert.DoesNotContain("Buy eggs", uiMock.output);
        }
    }
}
