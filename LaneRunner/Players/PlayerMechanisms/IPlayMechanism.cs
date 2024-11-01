using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Weapons;

namespace LaneRunner.Players.PlayerMechanisms
{
    internal interface IPlayMechanism
    {
        public int GetUpdatedPosition(int currentPosition,
            int gridWidth,
            Grid<Collideable> collideablesGrid);
        public bool IsFiringWeapon(Grid<Player> playerGrid, 
            Grid<Collideable> collideablesGrid, 
            Grid<WeaponShot> weaponGrid,
            IWeapon weapon);
    }
}
