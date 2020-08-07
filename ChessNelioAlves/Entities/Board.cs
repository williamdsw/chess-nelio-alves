
using Entities.Exceptions;

namespace Entities
{
    public class Board
    {
        // FIELDS

        private Piece[,] pieces;

        // PROPERTIES

        public int Rows { get; set; }
        public int Columns { get; set; }

        // CONSTRUCTORS

        public Board() { }
        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        // FUNCTIONS

        public Piece GetPieceAt(int row, int column)
        {
            return pieces[row, column];
        }

        public Piece GetPieceAt(Position position)
        {
            return pieces[position.Row, position.Column];
        }

        public void InsertPieceAt(Piece piece, Position position)
        {
            if (PieceExists(position))
            {
                throw new ChessException($"A piece already exists at position {position}!");
            }

            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePieceAt(Position position)
        {
            if (GetPieceAt(position) == null)
            {
                return null;
            }

            Piece piece = GetPieceAt(position);
            piece.Position = null;
            pieces[position.Row, position.Column] = null;
            return piece;
        }

        public bool PositionExists (Position position)
        {
            return !(position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns);
        }

        public bool PieceExists(Position position)
        {
            ValidatePosition(position);
            return GetPieceAt(position) != null;
        }

        public void ValidatePosition(Position position)
        {
            if (!PositionExists(position))
            {
                throw new ChessException($"Invalid Position at {position} !");
            }
        }
    }
}
