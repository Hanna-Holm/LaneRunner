
using Raylib_cs;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class DamageEffect : ICollisionEffect
    {
        private readonly int _amount = 1;
        public Color Color { get; } = Color.Red;
        public Texture2D Texture { get; }

        public DamageEffect(Texture2D texture)
        {
            Texture = texture;
        }

        public void ApplyCollisionEffect(IPlayer player)
        {
            player.TakeDamage(_amount);
        }
    }
}
