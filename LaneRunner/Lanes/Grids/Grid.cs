using System.Collections;

namespace LaneRunner.Lanes.Grids
{
    /*
       KRAV 1: 
       1. Koncept: Generics.
       2. Hur: 
       3. Varför: 
    */
    /*
       KRAV 4:
       1. Koncept: Enumerable & enumerator.
       2. Hur:
       3. Varför:
    */
    internal class Grid<T> : IEnumerable<GridItem<T>>
    {
        public int Columns { get; }
        public int Rows { get; }
        private GridItem<T>[,] _cells;

        public Grid(int numberOfColumns, int numberOfRows)
        {
            Columns = numberOfColumns;
            Rows = numberOfRows;
            _cells = new GridItem<T>[Columns, Rows];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<GridItem<T>> GetEnumerator()
        {
            for (int rowNumber = 0; rowNumber < Rows; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < Columns; columnNumber++)
                {
                    yield return _cells[columnNumber, rowNumber];
                }
            }
        }

        public void SetCellValue(int columnNumber, int rowNumber, T value)
        {
            _cells[columnNumber, rowNumber] = new GridItem<T>(columnNumber, rowNumber, value);
        }

        public void RemoveGridItem(int columnNumber, int rowNumber)
        {
            _cells[columnNumber, rowNumber] = null;
        }

        public GridItem<T> GetCellValue(int columnNumber, int rowNumber)
        {
            return _cells[columnNumber, rowNumber];
        }
    }
}
