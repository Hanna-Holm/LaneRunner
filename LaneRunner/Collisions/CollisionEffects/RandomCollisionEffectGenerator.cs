
using Raylib_cs;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class RandomCollisionEffectGenerator
    {
        private Random randomizer = new Random();

        public ICollisionEffect GetRandomCollisionEffect()
        {
            Texture2D weaponTexture = Raylib.LoadTexture("../../../UI/Assets/weapon-rot.PNG"); ;
            Texture2D damageTexture = Raylib.LoadTexture("../../../UI/Assets/bomb-shine.PNG"); ;
            Texture2D immunityTexture = Raylib.LoadTexture("../../../UI/Assets/apple.PNG");

            ICollisionEffect[] _collisionEffects =
                [
                    new EnableWeaponEffect(weaponTexture),
                    new DamageEffect(damageTexture),
                    new ImmunityEffect(immunityTexture)
                ];

            return _collisionEffects[randomizer.Next(0, _collisionEffects.Length)];
        }
    }
}
