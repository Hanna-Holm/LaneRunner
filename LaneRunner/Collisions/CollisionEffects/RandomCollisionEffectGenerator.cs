
using Raylib_cs;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class RandomCollisionEffectGenerator
    {
        private Random randomizer = new Random();
        private Texture2D _weaponTexture;
        private Texture2D _damageTexture;
        private Texture2D _immunityTexture;

        public void LoadTextures()
        {
            _weaponTexture = Raylib.LoadTexture("../../../UI/Assets/weapon-rot.PNG"); ;
            _damageTexture = Raylib.LoadTexture("../../../UI/Assets/bomb-shine.PNG"); ;
            _immunityTexture = Raylib.LoadTexture("../../../UI/Assets/apple.PNG");
        }

        public ICollisionEffect GetRandomCollisionEffect()
        {

            ICollisionEffect[] _collisionEffects =
                [
                    new EnableWeaponEffect(_weaponTexture),
                    new DamageEffect(_damageTexture),
                    new ImmunityEffect(_immunityTexture)
                ];

            return _collisionEffects[randomizer.Next(0, _collisionEffects.Length)];
        }
    }
}
