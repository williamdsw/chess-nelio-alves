
namespace Entities
{
    public class BoardPosition
    {
        // PROPERTIES

        public int Row { get; set; }
        public char Column { get; set; }

        // CONSTRUCTOR

        public BoardPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        // FUNCTIONS

        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column.ToString().ToUpper()}{Row}";
        }
    }
}
