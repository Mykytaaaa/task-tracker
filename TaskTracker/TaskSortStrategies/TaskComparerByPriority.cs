namespace TaskTracker.TaskSortStrategies
{
    public class TaskComparerByPriority : IComparer<Task>
    {
        public int Compare(Task? x, Task? y)
        {
            bool xHasPriority = x != null && x.HasPriority();
            bool yHasPriority = y != null && y.HasPriority();

            if (xHasPriority && yHasPriority)
                return x.Priority.CompareTo(y.Priority);
            else if (xHasPriority && !yHasPriority)
                return -1;
            else if (!xHasPriority && yHasPriority)
                return 1;
            else
                return 0;
        }
    }
}
