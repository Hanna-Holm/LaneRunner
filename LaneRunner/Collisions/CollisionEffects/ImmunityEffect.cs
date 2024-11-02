using Raylib_cs;
using LaneRunner.Players;

namespace LaneRunner.Collisions.CollisionEffects
{
    internal class ImmunityEffect : ICollisionEffect
    {
        public Color Color { get; } = Color.DarkBlue;
        public Texture2D Texture { get; set;  }
        private int _amount = 1;

        public ImmunityEffect(Texture2D texture)
        {
            Texture = texture;
        }

        public void ApplyCollisionEffect(IPlayer player)
        {
            Console.WriteLine("You are now immune for one collision!");
            player.AddImmunity(_amount);
        }
    }
}
