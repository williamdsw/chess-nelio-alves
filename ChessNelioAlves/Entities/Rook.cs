
namespace Entities
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] validPositions = new bool[Board.Rows, Board.Columns];
            int originalRow = this.Position.Row;
            int originalCol = this.Position.Column;

            Position position = new Position(originalRow, originalCol);

            // up
            position.DefineValues(position.Row - 1, position.Column);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.Row--;
            }

            // down
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row + 1, position.Column);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.Row++;
            }

            // right
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row, position.Column + 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.Column++;
            }

            // left
            position = new Position(originalRow, originalCol);
            position.DefineValues(position.Row, position.Column - 1);
            while (Board.PositionExists(position) && CanMoveToPosition(position))
            {
                validPositions[position.Row, position.Column] = true;

                Piece piece = Board.GetPieceAt(position);
                if (piece != null && piece.Color != this.Color)
                {
                    break;
                }

                position.Column--;
            }

            return validPositions;
        }
    }
}
