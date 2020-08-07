
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

        public abstract bool[,] PossibleMovements();

        protected bool CanMoveToPosition(Position position)
        {
            Piece piece = Board.GetPieceAt(position);
            return piece == null || piece.Color != this.Color;
        }
    }
}
