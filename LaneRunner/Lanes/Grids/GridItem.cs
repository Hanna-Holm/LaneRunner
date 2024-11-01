namespace LaneRunner.Lanes.Grids
{
    internal class GridItem<T>
    {
        public int XPosition { get; private set; }
        public int YPosition { get; private set; }
        public T Value { get; set; }

        public GridItem(int x, int y, T value)
        {
            XPosition = x;
            YPosition = y;
            Value = value;
        }
    }
}
