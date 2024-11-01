using Raylib_cs;

namespace LaneRunner.UI.Rendering.WeaponShotRendering
{
    internal class LaserRenderer : ShotRenderer
    {
        private Color _colorOfShots = Color.Orange;

        public override void Render(int xPos, int yPos, int cellWidth, int cellHeight)
        {
            Raylib.DrawRectangle(xPos, yPos, cellWidth, cellHeight, _colorOfShots);
        }
    }
}
