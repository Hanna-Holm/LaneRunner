
using LaneRunner.Weapons.WeaponBehaviours;

namespace LaneRunner.Weapons
{
    internal class RandomWeaponBehaviourGenerator
    {
        private Random _random = new Random();
        private List<IWeaponBehaviour> _behaviours = new List<IWeaponBehaviour>
        {
            new BeamWeaponBehaviour(), 
            new WideSpreadWeaponBehavior()
        };

        public IWeaponBehaviour Generate()
        {
            return _behaviours[_random.Next(0, _behaviours.Count)];
        }
    }
}
