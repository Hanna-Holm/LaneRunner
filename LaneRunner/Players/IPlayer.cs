using LaneRunner.Collisions;
using LaneRunner.Players.PlayerMechanisms;
using LaneRunner.Weapons;

namespace LaneRunner.Players
{
    internal interface IPlayer
    {
        public Collider Collider { get; }
        public IWeapon Weapon { get; set; }
        public IPlayMechanism PlayMechanism { get; }
        public void TakeDamage(int amount);
        public void AddImmunity(int amount);
        public void AddWeapon(IWeapon weapon);
    }
}
