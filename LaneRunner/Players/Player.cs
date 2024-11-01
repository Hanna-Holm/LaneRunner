using Raylib_cs;
using LaneRunner.Weapons;
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players.PlayerMechanisms;

namespace LaneRunner.Players
{
    internal class Player : IPlayer
    {
        public IPlayMechanism PlayMechanism { get; }
        public Rectangle Shape;
        public int Health { get; private set; } = 3;
        private bool _isImmune = false;
        public IWeapon Weapon { get; set; }
        public bool HasWeapon = false;
        public Collider Collider { get; }

        public Player(IPlayMechanism playMechanism, Collider collider)
        {
            PlayMechanism = playMechanism;
            Collider = collider;

            Shape = new Rectangle
            {
                Height = 35,
                Width = 35,
            };
        }
        private Grid<Collideable> _collideablesGrid;

        public void Update(int x, int y, Grid<Player> playerGrid, Grid<WeaponShot> weaponGrid)
        {
            int gridWidth = playerGrid.Columns;
            int newXPosition = PlayMechanism.GetUpdatedPosition(x, gridWidth, _collideablesGrid);
            playerGrid.RemoveGridItem(x, y);
            playerGrid.SetCellValue(newXPosition, y, this);

            if (HasWeapon)
            {
                if (PlayMechanism.IsFiringWeapon(playerGrid, _collideablesGrid, weaponGrid, Weapon))
                {
                    HasWeapon = false;
                }
            }
        }

        public void GetAccessToCollideablesGrid(Grid<Collideable> collideablesGrid)
        {
            _collideablesGrid = collideablesGrid;
        }

        public void TakeDamage(int amount)
        {
            if (_isImmune)
            {
                Console.WriteLine("You got away from one damage by using your immunity!");
                _isImmune = false;
                return;
            }

            Health -= amount;
        }

        public void AddImmunity(int amount)
        {
            _isImmune = true;
        }

        public void AddWeapon(IWeapon weapon)
        {
            Weapon = weapon;
            Weapon.GetRandomWeaponBehaviour();
            HasWeapon = true;
        }
    }
}
