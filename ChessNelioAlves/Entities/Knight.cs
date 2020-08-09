
namespace Entities
{
    public class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
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

            position.DefineValues(position.Row - 1, position.Column - 2);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row - 2, position.Column  - 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row - 2, position.Column + 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = this.Position;
            position.DefineValues(position.Row - 1, position.Column + 2);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column + 2);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 2, position.Column + 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            // left
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 2, position.Column - 1);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column - 2);
            if (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;
            }

            this.Position = new Position(originalRow, originalCol);
            return validPositions;
        }
    }
}
