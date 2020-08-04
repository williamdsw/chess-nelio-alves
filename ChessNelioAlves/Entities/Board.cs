
namespace Entities
{
    public class Board
    {
        private Piece[,] pieces;

        public int Rows { get; set; }
        public int Columns { get; set; }

        public Board() { }
        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece GetPieceAt(int row, int column)
        {
            return pieces[row, column];
        }
    }
}
