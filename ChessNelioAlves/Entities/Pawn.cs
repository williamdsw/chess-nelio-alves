
namespace Entities
{
    public class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

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

                // #specialmove En Passant
                position = new Position(originalRow, originalCol);
                if (position.Row == 3)
                {
                    Position left = new Position(position.Row, position.Column - 1);
                    Piece leftPiece = Board.GetPieceAt(left);
                    if (Board.PositionExists(left) && CanMoveToPosition(left) && leftPiece == match.VunerableEnPassant)
                    {
                        validPositions[left.Row - 1, left.Column] = true;
                    }
                    
                    Position right = new Position(position.Row, position.Column + 1);
                    Piece rightPiece = Board.GetPieceAt(right);
                    if (Board.PositionExists(right) && CanMoveToPosition(right) && rightPiece == match.VunerableEnPassant)
                    {
                        validPositions[right.Row - 1, right.Column] = true;
                    }
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

                // #specialmove En Passant
                position = new Position(originalRow, originalCol);
                if (position.Row == 4)
                {
                    Position left = new Position(position.Row, position.Column - 1);
                    Piece leftPiece = Board.GetPieceAt(left);
                    if (Board.PositionExists(left) && CanMoveToPosition(left) && leftPiece == match.VunerableEnPassant)
                    {
                        validPositions[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(position.Row, position.Column + 1);
                    Piece rightPiece = Board.GetPieceAt(right);
                    if (Board.PositionExists(right) && CanMoveToPosition(right) && rightPiece == match.VunerableEnPassant)
                    {
                        validPositions[right.Row + 1, right.Column] = true;
                    }
                }
            }


            return validPositions;
        }
    }
}
