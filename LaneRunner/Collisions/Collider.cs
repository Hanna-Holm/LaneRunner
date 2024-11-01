using LaneRunner.Collisions.CollisionDetection;

namespace LaneRunner.Collisions
{
    internal class Collider
    {
        public ICollisionDetector CollisionDetector { get; }

        public Collider(ICollisionDetector collisionDetector)
        {
            CollisionDetector = collisionDetector;
        }
    }
}