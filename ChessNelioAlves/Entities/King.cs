
namespace Entities
{
    public class King : Piece
    {
        private ChessMatch match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool TestRookToRock(Position position)
        {
            Piece piece = Board.GetPieceAt(position);
            return (piece != null && piece is Rook && piece.Color == this.Color && piece.NumberOfMovements == 0);
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
            position = new Position(originalRow, originalCol);
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

            // #specialmove Rock
            if (NumberOfMovements == 0 && !match.IsInCheck)
            {
                // #specialmove Castling
                Position firstRookPosition = new Position(originalRow, originalCol + 3);
                if (TestRookToRock(firstRookPosition))
                {
                    position = new Position(originalRow, originalCol);
                    Position position1 = new Position(originalRow, originalCol + 1);
                    Position position2 = new Position(originalRow, originalCol + 2);

                    if (Board.GetPieceAt(position1) == null && Board.GetPieceAt(position2) == null)
                    {
                        validPositions[position.Row, position.Column + 2] = true;
                    }
                }

                // #specialmove Castling
                Position secondRookPosition = new Position(originalRow, originalCol - 4);
                if (TestRookToRock(secondRookPosition))
                {
                    position = new Position(originalRow, originalCol);
                    Position position1 = new Position(originalRow, originalCol - 1);
                    Position position2 = new Position(originalRow, originalCol - 2);
                    Position position3 = new Position(originalRow, originalCol - 3);

                    if (Board.GetPieceAt(position1) == null && Board.GetPieceAt(position2) == null && Board.GetPieceAt(position3) == null)
                    {
                        validPositions[position.Row, position.Column - 2] = true;
                    }
                }
            }

            this.Position = new Position(originalRow, originalCol);
            return validPositions;
        }
    }
}
