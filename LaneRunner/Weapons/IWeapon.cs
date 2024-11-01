
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.Weapons.WeaponBehaviours;

namespace LaneRunner.Weapons
{
    internal interface IWeapon
    {
        /*
           KRAV 3:
           1. Koncept: Bridge pattern.
           2. Hur: 
           3. Varför:
        */
        public IWeaponBehaviour WeaponBehaviour { get; }
        public RandomWeaponBehaviourGenerator WeaponBehaviourGenerator { get; }
        public void Fire(Grid<Player> playerGrid, Grid<WeaponShot> weaponShotGrid);
        public void GetRandomWeaponBehaviour();
        public void Update(Grid<WeaponShot> weaponShotGrid,
            Grid<Collideable> collideablesGrid,
            List<GridItem<WeaponShot>> shots);
    }
}
