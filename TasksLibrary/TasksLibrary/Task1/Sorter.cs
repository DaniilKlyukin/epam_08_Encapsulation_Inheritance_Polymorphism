namespace TasksLibrary
{
    using System.Linq;

    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    public delegate bool Comparer(int value1, int value2, Direction d);

    public abstract class Sorter : Evaluator
    {
        Comparer comp = (v1, v2, d) => v1.CompareTo(v2) == (int)d;

        private void SwapRows(int[,] array, int row1, int row2)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                var temp = array[row1, i];
                array[row1, i] = array[row2, i];
                array[row2, i] = temp;
            }
        }

        /// <summary>
        /// Evaluates rows using the specified evaluating method.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="evaluate">Evaluation method.</param>
        /// <returns>Rows estimates.</returns>
        private RowInfo[] CalculateInfo(int[,] arr, Evaluator evaluator)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            var rowsInfo = new RowInfo[rows];

            for (int i = 0; i < rows; i++)
                rowsInfo[i] = new RowInfo(i, evaluator.Evaluate(arr, i));

            return rowsInfo;
        }

        /// <summary>
        /// Bubble matrix sorting by rows based on information about them.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="rowsInfo">Row information i.e.data that evaluate the row.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        protected void BubbleSort(int[,] arr, Evaluator sortClass, Direction d)
        {
            var rowsInfo = CalculateInfo(arr, sortClass);

            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            for (int i = 1; i < rows; i++)
                for (int j = 0; j < i; j++)
                {
                    for (int v = 0; v < columns; v++)
                    {
                        var condition = comp(rowsInfo[j].RowEigenvalues[v], rowsInfo[i].RowEigenvalues[v], d);

                        if (condition)
                            Swap(rowsInfo, i, j);

                        if (rowsInfo[j].RowEigenvalues[v] != rowsInfo[i].RowEigenvalues[v])
                            break;
                    }
                }

            for (int i = 0; i < rows; i++)
                if (rowsInfo[i].RowIndex != i)
                {
                    SwapRows(arr, i, rowsInfo[i].RowIndex);

                    var rowInfo = rowsInfo.Single(x => x.RowIndex == i);
                    var temp = rowInfo.RowIndex;

                    rowInfo.RowIndex = rowsInfo[i].RowIndex;
                    rowsInfo[i].RowIndex = temp;
                }
        }

        private class RowInfo
        {
            public int RowIndex { get; set; }
            public int[] RowEigenvalues { get; set; }

            public RowInfo(int index, int[] value)
            {
                RowIndex = index;
                RowEigenvalues = value;
            }
        }
    }

    public class SortByRowSum : Sorter, ISortable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the sum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Sort(int[,] arr, Direction d)
        {
            BubbleSort(arr, this, d);
        }

        public override int[] Evaluate(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var sum = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                sum[0] += arr[row, i];

            return sum.Select(x => sum[0]).ToArray();
        }
    }

    public class SortByRowMaxElement : Sorter, ISortable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the maximum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Sort(int[,] arr, Direction d)
        {
            BubbleSort(arr, this, d);
        }

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

    public class SortByRowMinElement : Sorter, ISortable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the minimum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Sort(int[,] arr, Direction d)
        {
            BubbleSort(arr, this, d);
        }

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
}
