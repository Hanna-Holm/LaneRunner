
using LaneRunner.Collisions;
using LaneRunner.Lanes.Grids;
using LaneRunner.Players;
using LaneRunner.Weapons.WeaponBehaviours;

namespace LaneRunner.Weapons
{
    internal interface IWeapon
    {
        /*
           KRAV 3:
           1. Koncept: Bridge pattern.
           2. Hur: Vårt abstrakta interface IWeapon är komponerad av en annan abstraktion IWeaponBehaviour.
                Både IWeapon och IWeaponBehaviour har egna konkreta subklasser, och vi injicerar en konkret 
                subklass av IWeaponBehaviour in i en konkret subklass av IWeapon via metodinjektion.
                Denna konkreta subklassinstans tilldelas då till egenskapen WeaponBehaviour.
           3. Varför: Detta gör att olika konkretioner (subtyper) av IWeapon kan ha olika konkretioner
                av IWeaponBehaviour. Detta ger ökad flexibilitet eftersom vi slipper skapa subklasser
                för varje kombination av olika konkreta vapen och vapenbeteenden. Istället injicerar
                vi in vilket konkret IWeaponBehaviour vi vill ha in i ett konkret IWeapon. Det blir också 
                väldigt enkelt att utöka vårt program om vi vill ha fler olika vapen och vapenbeteenden,
                eftersom vi endast behöver lägga till en klass för vardera, istället för en klass för 
                varje möjlig kombination av vapen och vapenbeteende. Sedan gäller samma fördelar som för 
                strategy pattern, att vi decouplar implementationen från användandet vilket ger ökad
                flexibilitet och gör det enkelt att modifiera de olika konkretionerna oberoende från
                varandra och den andra abstrakta hierarkin.
        */
        public IWeaponBehaviour WeaponBehaviour { get; }
        public RandomWeaponBehaviourGenerator WeaponBehaviourGenerator { get; }
        public void Fire(Grid<Player> playerGrid, Grid<WeaponShot> weaponShotGrid);
        public void GetRandomWeaponBehaviour();
        public void Update(Grid<WeaponShot> weaponShotGrid,
            Grid<Collideable> collideablesGrid,
            List<GridItem<WeaponShot>> shots);
    }
}
