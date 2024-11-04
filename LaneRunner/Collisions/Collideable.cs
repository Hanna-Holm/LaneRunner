using Raylib_cs;
using LaneRunner.Collisions.CollisionEffects;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;

namespace LaneRunner.Collisions
{
    internal class Collideable
    {
        /*
           KRAV 2.
           1. Koncept: Strategy pattern.
           2. Hur: Collideable-klassen (kontextet) är komponerad av en abstraktion ICollisionEffect 
                som är en abstrakt supertyp mer flera olika konkretioner i form av konkreta
                subklasser (tex EnableWeaponEffect, DamageEffect och ImmunityEffect).
                ICollisionEffect är alltså vår abstrakta strategi, de olika subklasserna till
                ICollisionEffect är våra konkreta strategier och Collideable är kontextet i
                vilket dessa olika strategier används. Vi injicerar en konkret subtypsinstans via 
                konstruktorinjektion i runtime vars värde tilldelas egenskapen CollisionEffect.
           3. Varför: Detta gör att vi kan skicka in vilken som helst av våra konkreta
                strategier in i vårt kontext och tilldela till CollisionEffect-egenskapen. De 
                olika konkreta strategierna är utbytbara och vi kan alltså bestämma vilken 
                konkret strategi som vårt kontext Collideable ska ha i runtime istället för i 
                compile-time. Genom att decoupla implementationen av de olika strategierna från
                kontextet gör vi att de kan modifieras och uppdateras oberoende av varandra, och
                vi kan enkelt modifiera/lägga till/ta bort konkreta strategier utan att vare sig 
                kontextet eller de övriga konkreta strategierna påverkas. 
        */
        public ICollisionEffect CollisionEffect { get; }
        public Color Color { get; private set; }

        public Collideable(ICollisionEffect effect)
        {
            CollisionEffect = effect;
            Color = CollisionEffect.Color;
        }

        public void Update(int x, int y, Grid<Collideable> grid)
        {
            grid.RemoveGridItem(x, y);

            if (y < grid.Rows - 1)
            {
                int newYPosition = ++y;
                grid.SetCellValue(x, newYPosition, this);
            }
        }

        public void ApplyCollisionEffect(IPlayer player)
        {
            CollisionEffect.ApplyCollisionEffect(player);
        }
    }
}
