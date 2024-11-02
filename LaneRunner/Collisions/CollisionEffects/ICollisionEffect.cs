using Raylib_cs;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal interface ICollisionEffect
    {
        public Color Color { get; }
        public Texture2D Texture { get; }
        public void ApplyCollisionEffect(IPlayer player);
    }
}
