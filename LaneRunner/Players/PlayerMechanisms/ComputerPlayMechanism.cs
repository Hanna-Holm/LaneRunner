using Raylib_cs;
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Weapons;

namespace LaneRunner.Players.PlayerMechanisms
{
    internal class ComputerPlayMechanism : IPlayMechanism
    {
        private int _currentXPos;
        private int _updateTimer = 0;
        private int _updateInterval = 100;
        private Random _random = new Random();

        public int GetUpdatedPosition(int currentPosition,
            int gridWidth,
            Grid<Collideable> collideablesGrid)
        {
            _currentXPos = currentPosition;

            if (currentPosition > 0
                && currentPosition < gridWidth / 2
                && CheckIfDamagingObjectsAhead(collideablesGrid))
            {
                return --currentPosition;
            }
            else if (currentPosition >= gridWidth / 2
                && currentPosition < gridWidth - 1
                && CheckIfDamagingObjectsAhead(collideablesGrid))
            {
                return ++currentPosition;
            }
            else
            {
                return currentPosition;
            }
        }

        public bool IsFiringWeapon(Grid<Player> playerGrid,
            Grid<Collideable> collideablesGrid,
            Grid<WeaponShot> weaponShotGrid,
            IWeapon weapon)
        {
            if (CheckIfDamagingObjectsAhead(collideablesGrid))
            {
                weapon.Fire(playerGrid, weaponShotGrid);
                return true;
            }

            return false;
        }

        private bool CheckIfDamagingObjectsAhead(Grid<Collideable> collideablesGrid)
        {
            var collideablesInSameColumn = collideablesGrid
                .Where(x => x != null)
                .Where(x => x.XPosition == _currentXPos)
                .OrderByDescending(x => x.YPosition).ToList();

            _updateTimer += _random.Next(0, 4);

            if (_updateTimer > _updateInterval)
            {
                _updateTimer = 0;

                foreach (var collideable in collideablesInSameColumn)
                {
                    if (collideable.Value.CollisionEffect.Color.Equals(Color.Red))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
