namespace TaskTracker.TaskSortStrategies
{
    public class TaskComparerByPriority : IComparer<Task>
    {
        public int Compare(Task? x, Task? y)
        {
            bool xHasPriority = x != null && x.Priority != null;
            bool yHasPriority = y != null && y.Priority != null;

            if (xHasPriority && yHasPriority)
            {
                int notNullablePriorityX = (int)x.Priority;
                int notNullablePriorityY = (int)y.Priority;
                return notNullablePriorityX.CompareTo(notNullablePriorityY);
            }
            else if (xHasPriority && !yHasPriority)
                return -1;
            else if (!xHasPriority && yHasPriority)
                return 1;
            else
                return 0;
        }
    }
}
