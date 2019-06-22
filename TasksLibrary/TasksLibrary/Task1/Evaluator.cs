namespace TasksLibrary
{
    public abstract class Evaluator
    {
        public void BubbleSort(int[] arr, Direction d)
        {
            for (int i = 1; i < arr.Length; i++)
                for (int j = 0; j < i; j++)
                {
                    if (arr[j].CompareTo(arr[i]) == (int)d)
                        Swap(arr, i, j);
                }
        }

        protected void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public abstract int[] Evaluate(int[,] arr, int row);
    }
}
