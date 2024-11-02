using LaneRunner.Collisions.CollisionEffects;
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.Weapons;
using Raylib_cs;

namespace LaneRunner.Lanes
{
    internal class Lane
    {
        public Player Player { get; }
        public Grid<Player> PlayerGrid { get; }
        private readonly int _startColumnOfPlayer;
        private readonly int _startRowOfPlayer;
        public Grid<Collideable> CollideablesGrid { get; }
        public Grid<WeaponShot> WeaponShotsGrid { get; }
        public int OriginX { get; }
        public int OriginY { get; }

        private float _updateCollideablePositionTimer = 0;
        private double _updateCollideablePositionInterval = 0.4;
        private float _spawnCollideablesTimer = 0;
        private double _spawnCollideablesInterval = 1.2;
        private float _updateWeaponShotsPositionTimer = 0;
        private double _updateWeaponShotsPositionInterval = 0.05;

        private Random randomizer = new Random();
        private RandomCollisionEffectGenerator _collisionEffectGenerator
            = new RandomCollisionEffectGenerator();

        public Lane(Player player, int numberOfColumns, int numberOfRows, int originX, int originY)
        {
            OriginX = originX;
            OriginY = originY;

            PlayerGrid = new Grid<Player>(numberOfColumns, numberOfRows);
            _startColumnOfPlayer = numberOfColumns / 2;
            _startRowOfPlayer = numberOfRows - 1;
            Player = player;
            PlayerGrid.SetCellValue(_startColumnOfPlayer, _startRowOfPlayer, Player);

            CollideablesGrid = new Grid<Collideable>(numberOfColumns, numberOfRows);

            Player.GetAccessToCollideablesGrid(CollideablesGrid);

            WeaponShotsGrid = new Grid<WeaponShot>(numberOfColumns, numberOfRows);
        }

        public void Update(float secondsSinceLastFrame)
        {
            UpdatePlayerGrid();

            _updateCollideablePositionTimer += secondsSinceLastFrame;
            _spawnCollideablesTimer += secondsSinceLastFrame;
            _updateWeaponShotsPositionTimer += secondsSinceLastFrame;

            if (_updateCollideablePositionTimer >= _updateCollideablePositionInterval)
            {
                UpdateCollideablesGrid();
                _updateCollideablePositionTimer = 0;
                Player.Collider.CollisionDetector.CheckCollision(PlayerGrid, CollideablesGrid);
            }

            if (_spawnCollideablesTimer >= _spawnCollideablesInterval)
            {
                SpawnCollideables();
                _spawnCollideablesTimer = 0;
            }

            if (_updateWeaponShotsPositionTimer >= _updateWeaponShotsPositionInterval)
            {
                UpdateWeaponGrid();
                _updateWeaponShotsPositionTimer = 0;
            }
        }

        private void UpdatePlayerGrid()
        {
            var gridItems = PlayerGrid.Where(item => item != null);
            var players = new List<GridItem<Player>>();

            foreach (var item in gridItems)
            {
                players.Add(item);
            }

            foreach (var player in players)
            {
                player.Value.Update(player.XPosition, player.YPosition, PlayerGrid, WeaponShotsGrid);
            }
        }

        private void UpdateCollideablesGrid()
        {
            var gridItems = CollideablesGrid.Where(item => item != null);
            var collideables = new List<GridItem<Collideable>>();

            foreach (var item in gridItems)
            {
                collideables.Add(item);
            }

            foreach (var collideable in collideables)
            {
                collideable.Value.Update(collideable.XPosition, collideable.YPosition, CollideablesGrid);
            }
        }

        private void SpawnCollideables()
        {
            int randomXValue = randomizer.Next(0, CollideablesGrid.Columns);
            var randomCollisionEffect = _collisionEffectGenerator.GetRandomCollisionEffect();
            CollideablesGrid.SetCellValue(randomXValue, 0, new Collideable(randomCollisionEffect));
        }

        private void UpdateWeaponGrid()
        {
            var shots = WeaponShotsGrid.Where(x => x != null)
                .OrderByDescending(x => x.YPosition)
                .ToList();

            if (shots.Count > 0)
            {
                Player.Weapon.Update(WeaponShotsGrid, CollideablesGrid, shots);
            }
        }
    }
}
