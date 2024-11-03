using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionDetection
{
    internal class CellCollisionDetector : ICollisionDetector
    {
        public bool CheckCollision(Grid<Player> playerGrid, Grid<Collideable> collideablesGrid)
        {
            foreach (var collideableItem in collideablesGrid)
            {
                if (collideableItem != null
                    && playerGrid.GetCellValue(collideableItem.XPosition, 
                    collideableItem.YPosition) != null)
                {
                    var player = playerGrid.GetCellValue(
                        collideableItem.XPosition, collideableItem.YPosition).Value;
                    collideableItem.Value.ApplyCollisionEffect(player);
                    collideablesGrid.RemoveGridItem(collideableItem.XPosition, collideableItem.YPosition);
                    return true;
                }
            }

            return false;
        }
    }
}
