
namespace Entities
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] validPositions = new bool[Board.Rows, Board.Columns];
            int originalRow = this.Position.Row;
            int originalCol = this.Position.Column;
            Position position = new Position(originalRow, originalCol);

            // up
            position.DefineValues(position.Row - 1, position.Column);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // northeast
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row - 1, position.Column + 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // right
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row, position.Column + 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // southeast
            position = this.Position;
            position.DefineValues(position.Row + 1, position.Column + 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // down
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // southwest
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column - 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // left
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row, position.Column - 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // northwest
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row - 1, position.Column - 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            this.Position = new Position(originalRow, originalCol);
            return validPositions;
        }
    }
}
