
namespace Entities
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece() { }
        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            NumberOfMovements = 0;
        }

        public void IncrementNumberOfMovements()
        {
            NumberOfMovements++;
        }

        public void DecrementNumberOfMovements()
        {
            NumberOfMovements--;
        }

        public abstract bool[,] PossibleMovements();

        protected virtual bool CanMoveToPosition(Position position)
        {
            Piece piece = Board.GetPieceAt(position);
            return piece == null || piece.Color != this.Color;
        }

        public bool PossibleMovementsExists()
        {
            bool[,] possibleMovements = PossibleMovements();
            for (int row = 0; row < Board.Rows; row++)
            {
                for (int col = 0; col < Board.Columns; col++)
                {
                    if (possibleMovements[row, col])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }
    }
}
