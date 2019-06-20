namespace TasksLibrary
{
    using System.Linq;

    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    public abstract class Sorter : Evaluator
    {
        protected void SwapRows(int[,] array, int row1, int row2)
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
        protected RowInfo[] CalculateInfo(int[,] arr, Evaluator evaluator)
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
        protected void BubbleSort(int[,] arr, RowInfo[] rowsInfo, Direction d)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            for (int i = 1; i < rows; i++)
                for (int j = 0; j < i; j++)
                {
                    for (int v = 0; v < columns; v++)
                    {
                        var condition = rowsInfo[j].RowEigenvalues[v].CompareTo(rowsInfo[i].RowEigenvalues[v]);

                        if (condition == (int)d)
                            Swap(rowsInfo, i, j);

                        if (condition != 0)
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

        protected class RowInfo
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
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, new SumEvaluator());
            BubbleSort(arr, info, d);
        }
    }

    public class SortByRowMaxElement : Sorter, ISortable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the maximum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, new MaxEvaluator());
            BubbleSort(arr, info, d);
        }
    }

    public class SortByRowMinElement : Sorter, ISortable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the minimum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, new MinEvaluator());
            BubbleSort(arr, info, d);
        }
    }
}
