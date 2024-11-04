
using Raylib_cs;
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.UI.Rendering.WeaponShotRendering;
using LaneRunner.Weapons.WeaponBehaviours;

namespace LaneRunner.Weapons
{
    internal class BulletWeapon : IWeapon
    {
        public IWeaponBehaviour WeaponBehaviour { get; private set; }
        public RandomWeaponBehaviourGenerator WeaponBehaviourGenerator { get; } = new();
        private BulletRenderer _bulletRenderer = new BulletRenderer();
        public WeaponShot WeaponShot;

        public BulletWeapon()
        {
            WeaponShot = new WeaponShot(_bulletRenderer);
            WeaponBehaviour = WeaponBehaviourGenerator.Generate();
        }

        public void GetRandomWeaponBehaviour()
        {
            WeaponBehaviour = WeaponBehaviourGenerator.Generate();
        }

        public void Fire(Grid<Player> playerGrid, Grid<WeaponShot> weaponShotGrid)
        {
            var playerXPosition = playerGrid.Where(x => x != null).FirstOrDefault().XPosition;
            WeaponBehaviour.Projectile(playerXPosition, weaponShotGrid, WeaponShot); 
        }

        public void Update(Grid<WeaponShot> weaponShotGrid,
            Grid<Collideable> collideablesGrid,
            List<GridItem<WeaponShot>> shots)
        {
            foreach (var shot in shots)
            {
                if (shot.YPosition > 0)
                {
                    int newYPos = shot.YPosition - 1;
                    weaponShotGrid.SetCellValue(shot.XPosition, newYPos, new WeaponShot(_bulletRenderer));

                    if (collideablesGrid.GetCellValue(shot.XPosition, newYPos) != null)
                    {
                        collideablesGrid.RemoveGridItem(shot.XPosition, newYPos);
                        weaponShotGrid.RemoveGridItem(shot.XPosition, newYPos);
                    }

                    if (collideablesGrid.GetCellValue(shot.XPosition, shot.XPosition) != null)
                    {
                        collideablesGrid.RemoveGridItem(shot.XPosition, shot.XPosition);
                    }
                }

                weaponShotGrid.RemoveGridItem(shot.XPosition, shot.YPosition);
            }
        }
    }
}
