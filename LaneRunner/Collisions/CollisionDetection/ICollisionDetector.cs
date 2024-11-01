using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionDetection
{
    internal interface ICollisionDetector
    {
        public bool CheckCollision(Grid<Player> playerGrid, Grid<Collideable> collideablesGrid);
    }
}
