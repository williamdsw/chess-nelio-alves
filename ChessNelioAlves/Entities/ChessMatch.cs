
namespace Entities
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        private int turn;
        private Color actualPlayer;
        public bool EndMatch { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            actualPlayer = Color.White;
            EndMatch = false;
            InsertPieces();
        }

        public void ExecuteMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePieceAt(origin);
            piece.IncrementNumberOfMovements();
            Piece otherPiece = Board.RemovePieceAt(destiny);
            Board.InsertPieceAt(piece, destiny);
        }

        private void InsertPieces ()
        {
            Board.InsertPieceAt(new Rook(Board, Color.White), new BoardPosition('c', 1).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.White), new BoardPosition('c', 2).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.White), new BoardPosition('d', 2).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.White), new BoardPosition('e', 2).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.White), new BoardPosition('e', 1).ToPosition());
            Board.InsertPieceAt(new King(Board, Color.White), new BoardPosition('d', 1).ToPosition());

            Board.InsertPieceAt(new Rook(Board, Color.Black), new BoardPosition('c', 7).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.Black), new BoardPosition('c', 8).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.Black), new BoardPosition('d', 7).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.Black), new BoardPosition('e', 7).ToPosition());
            Board.InsertPieceAt(new Rook(Board, Color.Black), new BoardPosition('e', 8).ToPosition());
            Board.InsertPieceAt(new King(Board, Color.Black), new BoardPosition('d', 8).ToPosition());
        }
    }
}
