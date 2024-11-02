
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.UI.Rendering.WeaponShotRendering;
using LaneRunner.Weapons.WeaponBehaviours;

namespace LaneRunner.Weapons
{
    internal class LaserWeapon : IWeapon
    {
        public IWeaponBehaviour WeaponBehaviour { get; private set; }
        public RandomWeaponBehaviourGenerator WeaponBehaviourGenerator { get; } = new();
        private LaserRenderer _laserRenderer = new LaserRenderer();
        public WeaponShot WeaponShot;
        private int _showVisualsTimer = 0;
        private int _removeVisualsThreshold = 4;

        public LaserWeapon()
        {
            WeaponShot = new WeaponShot(_laserRenderer);
            WeaponBehaviour = WeaponBehaviourGenerator.Generate();
        }

        public void GetRandomWeaponBehaviour()
        {
            WeaponBehaviour = WeaponBehaviourGenerator.Generate();
        }

        public void Fire(Grid<Player> playerGrid, Grid<WeaponShot> weaponShotGrid)
        {
            var playerXPosition = playerGrid.Where(x => x != null)
                .FirstOrDefault().XPosition;
            WeaponBehaviour.Projectile(playerXPosition, weaponShotGrid, WeaponShot);
            _showVisualsTimer = 0;
        }

        public void Update(Grid<WeaponShot> weaponShotGrid,
            Grid<Collideable> collideablesGrid,
            List<GridItem<WeaponShot>> shots)
        {
            foreach (var shot in shots)
            {
                int currentYPos = collideablesGrid.Rows - 1;

                while (currentYPos > 0)
                {
                    weaponShotGrid.SetCellValue(shot.XPosition, currentYPos, new WeaponShot(_laserRenderer));
                    int newYPos = currentYPos - 1;

                    if (collideablesGrid.GetCellValue(shot.XPosition, newYPos) != null)
                    {
                        collideablesGrid.RemoveGridItem(shot.XPosition, newYPos);
                        weaponShotGrid.RemoveGridItem(shot.XPosition, newYPos);
                    }

                    currentYPos -= 1;
                }
            }

            _showVisualsTimer += 1;

            if (_showVisualsTimer > _removeVisualsThreshold)
            {
                var weaponShotsToRemove = new List<WeaponShot>();

                foreach (var weaponShot in weaponShotGrid)
                {
                    if (weaponShot != null)
                    {
                        weaponShotsToRemove.Add(WeaponShot);
                    }
                }

                foreach (var weaponShot in weaponShotGrid)
                {
                    if (weaponShot != null)
                    {
                        weaponShotGrid.RemoveGridItem(weaponShot.XPosition, weaponShot.YPosition);
                    }
                }
            }
        }
    }
}
