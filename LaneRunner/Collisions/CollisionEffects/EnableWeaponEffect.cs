
using Raylib_cs;
using LaneRunner.Players;
using LaneRunner.Weapons;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class EnableWeaponEffect : ICollisionEffect
    {
        public Color Color { get; } = Color.Blue;
        public Texture2D Texture { get; }
        private Random _random = new Random();
        private List<IWeapon> _weapons = new List<IWeapon>
        {
            new BulletWeapon(),
            new LaserWeapon()
        };

        public EnableWeaponEffect(Texture2D texture)
        {
            Texture = texture;
        }

        public void ApplyCollisionEffect(IPlayer player)
        {
            player.AddWeapon(GetRandomWeapon());
        }

        private IWeapon GetRandomWeapon()
        {
            return _weapons[_random.Next(0, _weapons.Count)];
        }
    }
}
