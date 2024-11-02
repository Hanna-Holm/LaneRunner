
using Raylib_cs;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class RandomCollisionEffectGenerator
    {
        private Random randomizer = new Random();

        public ICollisionEffect GetRandomCollisionEffect()
        {
            Texture2D weaponTexture;
            Texture2D damageTexture;
            Texture2D immunityTexture = Raylib.LoadTexture("../../../UI/Assets/apple.PNG");

            ICollisionEffect[] _collisionEffects =
                [
                    new EnableWeaponEffect(immunityTexture),
                    new DamageEffect(immunityTexture),
                    new ImmunityEffect(immunityTexture)
                ];

            return _collisionEffects[randomizer.Next(0, _collisionEffects.Length)];
        }
    }
}
