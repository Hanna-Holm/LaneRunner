
using LaneRunner.UI.Rendering.WeaponShotRendering;

namespace LaneRunner.Weapons
{
    internal class WeaponShot
    {
        public ShotRenderer ShotRenderer;

        public WeaponShot(ShotRenderer renderer)
        {
            ShotRenderer = renderer;
        }
    }
}
