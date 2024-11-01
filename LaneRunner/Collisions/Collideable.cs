using Raylib_cs;
using LaneRunner.Collisions.CollisionEffects;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions
{
    internal class Collideable
    {
        /*
           KRAV 2.
           1. Koncept: Strategy pattern.
           2. Hur: 
           3. Varför:
        */
        public ICollisionEffect CollisionEffect { get; }
        public Color Color { get; private set; }
        public Rectangle Shape;

        public Collideable(ICollisionEffect effect)
        {
            CollisionEffect = effect;
            Color = CollisionEffect.Color;

            Shape = new Rectangle
            {
                Height = 35,
                Width = 35
            };
        }

        public void Update(int x, int y, Grid<Collideable> grid)
        {
            grid.RemoveGridItem(x, y);

            if (y < grid.Rows - 1)
            {
                int newYPosition = ++y;
                grid.SetCellValue(x, newYPosition, this);
            }
        }

        public void ApplyCollisionEffect(IPlayer player)
        {
            CollisionEffect.ApplyCollisionEffect(player);
        }
    }
}
