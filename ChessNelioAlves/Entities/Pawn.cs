
namespace Entities
{
    public class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "P";
        }

        protected override bool CanMoveToPosition(Position position)
        {
            Piece piece = Board.GetPieceAt(position);
            return piece != null && piece.Color != this.Color;
        }

        private bool IsFree(Position position)
        {
            return Board.GetPieceAt(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] validPositions = new bool[Board.Rows, Board.Columns];
            int originalRow = this.Position.Row;
            int originalCol = this.Position.Column;

            Position position = new Position(originalRow, originalCol);

            if (this.Color == Color.White)
            {
                position.DefineValues(position.Row - 1, position.Column);
                if (Board.PositionExists(position) && IsFree(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row - 2, position.Column);
                if (Board.PositionExists(position) && IsFree(position) && NumberOfMovements == 0)
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row - 1, position.Column - 1);
                if (Board.PositionExists(position) && CanMoveToPosition(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row - 1, position.Column + 1);
                if (Board.PositionExists(position) && CanMoveToPosition(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }
            }
            else
            {
                position.DefineValues(position.Row + 1, position.Column);
                if (Board.PositionExists(position) && IsFree(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row + 2, position.Column);
                if (Board.PositionExists(position) && IsFree(position) && NumberOfMovements == 0)
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row + 1, position.Column - 1);
                if (Board.PositionExists(position) && CanMoveToPosition(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }

                position = new Position(originalRow, originalCol);
                position.DefineValues(position.Row + 1, position.Column + 1);
                if (Board.PositionExists(position) && CanMoveToPosition(position))
                {
                    validPositions[position.Row, position.Column] = true;
                }
            }


            return validPositions;
        }
    }
}
