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

        private float _updateCollideablePositionTimer;
        private double _updateCollideablePositionInterval;
        private float _spawnCollideablesTimer;
        private double _spawnCollideablesInterval;
        private float _updateWeaponShotsPositionTimer;
        private readonly double _updateWeaponShotsPositionInterval = 0.05;
        private readonly int _levelTwoThreshold = 20;
        private readonly int _levelThreeThreshold = 40;
        private readonly int _levelFourThreshold = 60;
        private readonly int _levelFiveThreshold = 90;

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

            SetLevelOneSpeed();
        }

        public void LoadTextures()
        {
            _collisionEffectGenerator.LoadTextures();
        }

        public void Update(float secondsSinceLastFrame)
        {
            UpdatePlayerGrid();
            CheckIfNewLevel();

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

        private void CheckIfNewLevel()
        {
            if (Raylib.GetTime() > _levelTwoThreshold
                && Raylib.GetTime() < _levelTwoThreshold + 1)
            {
                SetLevelTwoSpeed();
            }
            else if (Raylib.GetTime() > _levelThreeThreshold
                && Raylib.GetTime() < _levelThreeThreshold + 1)
            {
                SetLevelThreeSpeed();
            }
            else if (Raylib.GetTime() > _levelFourThreshold
                && Raylib.GetTime() < _levelFourThreshold + 1)
            {
                SetLevelFourSpeed();
            }
            else if (Raylib.GetTime() > _levelFiveThreshold
                && Raylib.GetTime() < _levelFiveThreshold + 1)
            {
                SetLevelFiveSpeed();
            }
        }

        private void SetLevelOneSpeed()
        {
            _updateCollideablePositionInterval = 0.5;
            _spawnCollideablesInterval = 1.5;
        }

        private void SetLevelTwoSpeed()
        {
            _updateCollideablePositionInterval = 0.35;
            _spawnCollideablesInterval = 1.2;
        }

        private void SetLevelThreeSpeed()
        {
            _updateCollideablePositionInterval = 0.23;
            _spawnCollideablesInterval = 0.8;
        }

        private void SetLevelFourSpeed()
        {
            _updateCollideablePositionInterval = 0.16;
            _spawnCollideablesInterval = 0.5;
        }
        private void SetLevelFiveSpeed()
        {
            _updateCollideablePositionInterval = 0.1;
            _spawnCollideablesInterval = 0.2;
        }
    }
}
