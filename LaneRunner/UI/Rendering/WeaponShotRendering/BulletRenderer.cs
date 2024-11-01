
using Raylib_cs;

namespace LaneRunner.UI.Rendering.WeaponShotRendering
{
    internal class BulletRenderer : ShotRenderer
    {
        private Color _colorOfShots = Color.Black;

        public override void Render(int xPos, int yPos, int cellWidth, int cellHeight)
        {
            Raylib.DrawCircle(xPos + cellWidth / 2, yPos + cellHeight / 2, cellWidth / 2, _colorOfShots);
        }
    }
}
