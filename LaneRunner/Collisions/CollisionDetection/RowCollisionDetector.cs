using LaneRunner.Collisions.CollisionEffects;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionDetection
{
    internal class RowCollisionDetector : ICollisionDetector
    {
        public bool CheckCollision(Grid<Player> playerGrid, Grid<Collideable> collideablesGrid)
        {
            var playerPosition = playerGrid.Where(x => x != null).FirstOrDefault().XPosition;

            foreach (var cell in collideablesGrid)
            {
                if (cell.XPosition == playerPosition
                    && cell.Value != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
