using Raylib_cs;
using LaneRunner.Collisions;
using LaneRunner.Lanes;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.Weapons;

namespace LaneRunner.UI.Rendering
{
    internal class LaneRenderer
    {
        private readonly Color backgroundColor = Color.SkyBlue;
        private readonly int _cellWidth = 35;
        private readonly int _cellHeight = 35;
        private int _width;
        private int _height;
        private int _originX;
        private int _originY;

        private Texture2D _healthBar;
        private Texture2D _threeLivesTexture;
        private Texture2D _twoLivesTexture;
        private Texture2D _oneLifeTexture;
        private Texture2D _playerTexture;

        private int healthBarYPosition;
        private int _healthBarXPosition;

        private int _UIMessagesXPosition;
        private string _weaponMessage = "You got a weapon! Fire it by pressing <space>";
        private string _immunityMessage = "You are now immune for one collision!";

        public void InitializeTextures()
        {
            _threeLivesTexture = Raylib.LoadTexture("../../../UI/Assets/lives-three.PNG");
            _twoLivesTexture = Raylib.LoadTexture("../../../UI/Assets/lives-two.PNG");
            _oneLifeTexture = Raylib.LoadTexture("../../../UI/Assets/lives-one.PNG");
            _playerTexture = Raylib.LoadTexture("../../../UI/Assets/player-texture.PNG");
        }

        public void Render(Lane lane)
        {
            _width = lane.PlayerGrid.Columns * _cellWidth;
            _height = lane.PlayerGrid.Rows * _cellHeight;
            _originX = lane.OriginX;
            _originY = lane.OriginY;

            healthBarYPosition = _height + 55;
            _healthBarXPosition = _originX + _width / 3 - 10;
            _UIMessagesXPosition = _originX - _width / 4;

            RenderBackground(lane);
            RenderPlayer(lane.PlayerGrid, Color.DarkGray);
            RenderCollideables(lane.CollideablesGrid);
            RenderWeaponShots(lane.WeaponShotsGrid);
            RenderUIMessage(lane.PlayerGrid);
        }

        private void RenderBackground(Lane lane)
            => Raylib.DrawRectangle(lane.OriginX, lane.OriginY, _width, _height, backgroundColor);

        private void RenderPlayer(Grid<Player> playerGrid, Color cellColor)
        {
            /*
               KRAV 5:
               1. Koncept: LINQ method syntax.
               2. Hur: Vi använder LINQ-methoden .Where() på playerGrid som är en kollektion.
                    Vi skickar in ett argument i form av en delegat som vi uttrycker med ett
                    lambda expression: (item => item != null), som säger att vi vill filtrera
                    på de items som inte är null och endast ha kvar de värden som inte är det.
                    Själva exekveringen av queryt sker sedan först i foreach-loopen där vi 
                    faktiskt försöker nå elementen, eftersom har så kallad lazy evaluation
                    vilket betyder att LINQ bara skapar en query-definition vid användningen 
                    av olika LINQ-metoder, och utför inte operationerna förrän man efterfrågar
                    elementen.
               3: Varför: LINQ förenklar hantering av kollektioner genom att erbjuda en effektiv 
                    och koncis syntax för olika operationer, och är speciellt användbart vid
                    mer komplexa operationer. LINQ gör även att typ-säkerheten behålls.
            */
            var gridItems = playerGrid.Where(item => item != null);

            foreach (var item in gridItems)
            {
                int xPosition = _originX + item.XPosition * _cellWidth;
                int yPosition = _originY + item.YPosition * _cellHeight;
                Raylib.DrawTexture(_playerTexture, xPosition, yPosition, Color.White);
            }
        }

        private void RenderCollideables(Grid<Collideable> collideablesGrid)
        {
            var gridItems = collideablesGrid.Where(item => item != null);

            foreach (var item in gridItems)
            {
                int xPosition = _originX + item.XPosition * _cellWidth;
                int yPosition = _originY + item.YPosition * _cellHeight;

                Raylib.DrawTexture(item.Value.CollisionEffect.Texture, xPosition, yPosition, Color.White);
            }
        }

        private void RenderWeaponShots(Grid<WeaponShot> weaponShotsGrid)
        {
            var gridItems = weaponShotsGrid.Where(item => item != null);

            foreach (var item in gridItems)
            {
                int xPosition = _originX + item.XPosition * _cellWidth;
                int yPosition = _originY + item.YPosition * _cellHeight;

                item.Value.ShotRenderer.Render(xPosition, yPosition, _cellWidth, _cellHeight);
            }
        }

        private void RenderUIMessage(Grid<Player> playerGrid)
        {
            var player = playerGrid.Where(item => item != null).First();
            RenderWeaponMessage(player);
            RenderHealthBar(player);
            RenderImmunityMessage(player);
        }

        private void RenderWeaponMessage(GridItem<Player> player)
        {
            if (player.Value.HasWeapon == true)
            {
                Raylib.DrawText(_weaponMessage, _UIMessagesXPosition, _originY + _height + 10, 20, Color.DarkBlue);
            }
        }

        private void RenderHealthBar(GridItem<Player> player)
        {
            if (player.Value.Health == 3)
            {
                _healthBar = _threeLivesTexture;
            }
            else if (player.Value.Health == 2)
            {
                _healthBar = _twoLivesTexture;
            }
            else if (player.Value.Health == 1)
            {
                _healthBar = _oneLifeTexture;
            }

            Raylib.DrawTexture(_healthBar, _healthBarXPosition, healthBarYPosition, Color.White);
        }

        private void RenderImmunityMessage(GridItem<Player> player)
        {
            if (player.Value.IsImmune)
            {
                Raylib.DrawText(_immunityMessage, _UIMessagesXPosition, _originY + _height + 70, 20, Color.DarkBlue);
            }
        }

        public void UnloadTextures()
        {
            Raylib.UnloadTexture(_threeLivesTexture);
            Raylib.UnloadTexture(_twoLivesTexture);
            Raylib.UnloadTexture(_oneLifeTexture);
        }
    }
}
