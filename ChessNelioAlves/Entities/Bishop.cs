
namespace Entities
{
    public class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] validPositions = new bool[Board.Rows, Board.Columns];
            int originalRow = this.Position.Row;
            int originalCol = this.Position.Column;

            Position position = new Position(originalRow, originalCol);

            // NO
            position.DefineValues(position.Row - 1, position.Column - 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.DefineValues(position.Row - 1, position.Column - 1);
            }

            // NE
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row - 1, position.Column + 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.DefineValues(position.Row - 1, position.Column + 1);
            }

            // SE
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column + 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.DefineValues(position.Row + 1, position.Column + 1);
            }

            // SO
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column - 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.DefineValues(position.Row + 1, position.Column - 1);
            }

            return validPositions;
        }
    }
}
