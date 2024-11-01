
using Raylib_cs;
using LaneRunner.Players;
using LaneRunner.Weapons;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class EnableWeaponEffect : ICollisionEffect
    {
        public Color Color { get; } = Color.Blue;
        private Random _random = new Random();
        private List<IWeapon> _weapons = new List<IWeapon>
        {
            new BulletWeapon(),
            new LaserWeapon()
        };

        public void ApplyCollisionEffect(IPlayer player)
        {
            player.AddWeapon(GetRandomWeapon());
            Console.WriteLine("A weapon was enabled! Use it by pressing spacebar.");
        }

        private IWeapon GetRandomWeapon()
        {
            return _weapons[_random.Next(0, _weapons.Count)];
        }
    }
}
