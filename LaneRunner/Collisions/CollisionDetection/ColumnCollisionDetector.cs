using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionDetection
{
    internal class ColumnCollisionDetector : ICollisionDetector
    {
        public bool CheckCollision(Grid<Player> playerGrid, Grid<Collideable> collideablesGrid)
        {
            var playerPosition = playerGrid.Where(x => x != null).FirstOrDefault().XPosition;

            foreach (var cell in collideablesGrid)
            {
                if (cell.XPosition == playerPosition 
                    && cell != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
