using Raylib_cs;
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Weapons;

namespace LaneRunner.Players.PlayerMechanisms
{
    internal class HumanPlayMechanism : IPlayMechanism
    {
        public int GetUpdatedPosition(int currentPosition,
            int gridWidth,
            Grid<Collideable> collideablesGrid)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Left)
                && currentPosition > 0
                && currentPosition <= gridWidth - 1)
            {
                return --currentPosition;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Right)
                && currentPosition < gridWidth - 1
                && currentPosition >= 0)
            {
                return ++currentPosition;
            }

            return currentPosition;
        }

        public bool IsFiringWeapon(Grid<Player> playerGrid, 
            Grid<Collideable> collideablesGrid,
            Grid<WeaponShot> weaponShotGrid,
            IWeapon weapon)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Space)) 
            {
                weapon.Fire(playerGrid, weaponShotGrid);
                return true;
            }

            return false;
        }
    }
}
