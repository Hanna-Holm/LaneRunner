
using Raylib_cs;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class DamageEffect : ICollisionEffect
    {
        private readonly int _amount = 1;
        public Color Color { get; } = Color.Red;

        public void ApplyCollisionEffect(IPlayer player)
        {
            Console.WriteLine("Collided with a damaging object!");
            player.TakeDamage(_amount);
        }
    }
}
