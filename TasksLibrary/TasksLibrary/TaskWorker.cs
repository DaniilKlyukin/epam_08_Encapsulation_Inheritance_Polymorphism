namespace TasksLibrary
{
    public class TaskWorker
    {
        public void BubbleSort(int[,] array, ISortable sortMethod, Direction d)
            => sortMethod.Sort(array, d);
    }
}