
using LaneRunner.Lanes.Grids;

namespace LaneRunner.Weapons.WeaponBehaviours
{
    internal class BeamWeaponBehaviour : IWeaponBehaviour
    {
        public void Projectile(int playerXPosition, Grid<WeaponShot> weaponShotGrid, WeaponShot shot)
        {
            weaponShotGrid.SetCellValue(playerXPosition, weaponShotGrid.Rows - 1, shot);
        }
    }
}
