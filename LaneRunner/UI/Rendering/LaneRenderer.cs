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
               2. Hur: 
               3: Varför: 
            */
            var gridItems = playerGrid.Where(item => item != null).ToList();

            foreach (var item in gridItems)
            {
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;
                Raylib.DrawRectangle(xPosition, yPosition, _cellWidth, _cellHeight, cellColor);
            }
        }

        private void RenderCollideables(Grid<Collideable> collideablesGrid, int originX, int originY)
        {
            var gridItems = collideablesGrid.Where(item => item != null).ToList();

            foreach (var item in gridItems)
            {
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;

                Raylib.DrawRectangle(xPosition, yPosition, _cellWidth, _cellHeight, item.Value.Color);
            }
        }

        private void RenderWeaponShots(Grid<WeaponShot> weaponShotsGrid, int originX, int originY)
        {
            var gridItems = weaponShotsGrid.Where(item => item != null).ToList();

            foreach (var item in gridItems)
            {
                int xPosition = originX + item.XPosition * _cellWidth;
                int yPosition = originY + item.YPosition * _cellHeight;

                item.Value.ShotRenderer.Render(xPosition, yPosition, _cellWidth, _cellHeight);
            }
        }
    }
}
