
using LaneRunner.Lanes.Grids;

namespace LaneRunner.Weapons.WeaponBehaviours
{
    internal interface IWeaponBehaviour
    {
        public void Projectile(int playerXPosition, Grid<WeaponShot> weaponGrid, WeaponShot shot);
    }
}
