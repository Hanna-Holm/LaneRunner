
using LaneRunner.Lanes.Grids;

namespace LaneRunner.Weapons.WeaponBehaviours
{
    internal class WideSpreadWeaponBehavior : IWeaponBehaviour
    {
        public void Projectile(int xPosition, Grid<WeaponShot> weaponShotGrid, WeaponShot shot)
        {
            for (int column = 0; column < weaponShotGrid.Columns; column++)
            {
                weaponShotGrid.SetCellValue(column, weaponShotGrid.Rows - 1, shot);
            }
        }
    }
}
