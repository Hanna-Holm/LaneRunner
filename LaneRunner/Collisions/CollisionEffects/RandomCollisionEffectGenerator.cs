
namespace LaneRunner.Collisions.CollisionEffects
{
    internal class RandomCollisionEffectGenerator
    {
        private Random randomizer = new Random();
        private ICollisionEffect[] _collisionEffects =
        [
            new EnableWeaponEffect(),
            new DamageEffect(),
            new ImmunityEffect()
        ];

        public ICollisionEffect GetRandomCollisionEffect()
        {
            return _collisionEffects[randomizer.Next(0, _collisionEffects.Length)];
        }
    }
}
