namespace TasksLibrary
{
    using System.Linq;

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

        public virtual int[] Evaluate(int[,] arr, int row)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MaxEvaluator : Evaluator
    {
        public override int[] Evaluate(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var maximums = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                maximums[i] = arr[row, i];

            BubbleSort(maximums, Direction.Descending);

            return maximums;
        }
    }

    public class MinEvaluator : Evaluator
    {
        public override int[] Evaluate(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var minimums = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                minimums[i] = arr[row, i];

            BubbleSort(minimums, Direction.Ascending);

            return minimums;
        }
    }

    public class SumEvaluator : Evaluator
    {
        public override int[] Evaluate(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var sum = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                sum[0] += arr[row, i];

            return sum.Select(x => sum[0]).ToArray();
        }
    }
}
