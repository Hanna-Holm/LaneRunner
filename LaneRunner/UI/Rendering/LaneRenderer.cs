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

        public void Render(Lane lane)
        {
            _width = lane.PlayerGrid.Columns * _cellWidth;
            _height = lane.PlayerGrid.Rows * _cellHeight;

            RenderBackground(lane);
            RenderPlayer(lane.PlayerGrid, lane.OriginX, lane.OriginY, Color.DarkGray);
            RenderCollideables(lane.CollideablesGrid, lane.OriginX, lane.OriginY);
            RenderWeaponShots(lane.WeaponShotsGrid, lane.OriginX, lane.OriginY);
        }

        private void RenderBackground(Lane lane)
            => Raylib.DrawRectangle(lane.OriginX, lane.OriginY, _width, _height, backgroundColor);

        private void RenderPlayer(Grid<Player> playerGrid, int originX, int originY, Color cellColor)
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
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;
                Raylib.DrawRectangle(xPosition, yPosition, _cellWidth, _cellHeight, cellColor);
            }
        }

        private void RenderCollideables(Grid<Collideable> collideablesGrid, int originX, int originY)
        {
            var gridItems = collideablesGrid.Where(item => item != null);

            foreach (var item in gridItems)
            {
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;

                Raylib.DrawTexture(item.Value.CollisionEffect.Texture, xPosition, yPosition, Color.White);
            }
        }

        private void RenderWeaponShots(Grid<WeaponShot> weaponShotsGrid, int originX, int originY)
        {
            var gridItems = weaponShotsGrid.Where(item => item != null);

            foreach (var item in gridItems)
            {
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;

                item.Value.ShotRenderer.Render(xPosition, yPosition, _cellWidth, _cellHeight);
            }
        }
    }
}
