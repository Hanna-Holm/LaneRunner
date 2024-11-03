using System.Collections;

namespace LaneRunner.Lanes.Grids
{
    /*
       KRAV 1: 
       1. Koncept: Generics.
       2. Hur: Vår Grid-klass är en generisk klass och definieras av <> innehållande typ-
            parametern T, som är en placeholder för vilken typ som helst. Grid<T> är alltså  
            vår generiska typ-definition, och sedan konstruerar vi faktiska generiska typer 
            genom att skicka in en faktisk typ som typ-argument (vi gör detta i klassen Lane)  
            med Grid<Player>, Grid<Collideable> och Grid<WeaponShot> (där Player, Collideable 
            och WeaponShot utgör våra typ-argument).
       3. Varför: Detta gör att vi kan skapa en Grid med vilken typ som helst, eftersom Grid-
            klassen fungerar oavsett vad det är för typ vi konstruerar den med. Det finns två
            stora fördelar med detta, för det första slipper vi duplicera kod vilket annars 
            skulle bli fallet om vi tvingades skapa separata klasser för varje specifik typ 
            av grid vi kan tänkas ha. Varje separat grid-klass skulle då innehålla samma logik
            (med ett visst antal kolumner och rader, en 2D-array med celler/GridItems och 
            möjligheten att hämta/sätta/ta bort värden i specifika celler) vilket gör programmet 
            svårt att underhålla och modifiera eftersom vi då behöver ändra i samtliga klasser 
            istället för i bara en enda klass. För det andra får vi compile-time type checking
            vilket innebär en ökad typsäkerhet eftersom vi inte riskerar att få typ-relaterade 
            fel i run-time som kraschar vårt program!
    */
    /*
       KRAV 4:
       1. Koncept: Enumerable & enumerator.
       2. Hur: 
       3. Varför: 
    */
    internal class Grid<T> : IEnumerable<GridItem<T>>
    {
        public int Columns { get; }
        public int Rows { get; }
        private GridItem<T>[,] _cells;

        public Grid(int numberOfColumns, int numberOfRows)
        {
            Columns = numberOfColumns;
            Rows = numberOfRows;
            _cells = new GridItem<T>[Columns, Rows];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<GridItem<T>> GetEnumerator()
        {
            for (int rowNumber = 0; rowNumber < Rows; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < Columns; columnNumber++)
                {
                    yield return _cells[columnNumber, rowNumber];
                }
            }
        }

        public void SetCellValue(int columnNumber, int rowNumber, T value)
        {
            _cells[columnNumber, rowNumber] = new GridItem<T>(columnNumber, rowNumber, value);
        }

        public void RemoveGridItem(int columnNumber, int rowNumber)
        {
            _cells[columnNumber, rowNumber] = null;
        }

        public GridItem<T> GetCellValue(int columnNumber, int rowNumber)
        {
            return _cells[columnNumber, rowNumber];
        }
    }
}
