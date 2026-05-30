namespace TaskTracker.TaskSortStrategies
{
    public class TaskComparerByDueDate : IComparer<Task>
    {
        public int Compare(Task? x, Task? y)
        {
            bool xHasDueDate = x != null && x.HasDueDate();
            bool yHasDueDate = y != null && y.HasDueDate();

            if (xHasDueDate && yHasDueDate)
                return x.DueDate.CompareTo(y.DueDate);
            else if (xHasDueDate && !yHasDueDate)
                return -1;
            else if (!xHasDueDate && yHasDueDate)
                return 1;
            else
                return 0;
        }
    }
}
