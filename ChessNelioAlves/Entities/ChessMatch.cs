
using Entities.Exceptions;

namespace Entities
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool EndMatch { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
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

        public void PerformMove(Position origin, Position destiny)
        {
            ExecuteMovement(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            CurrentPlayer = (CurrentPlayer == Color.White ? Color.Black : Color.White);
        }

        public void ValidateOriginPosition(Position position)
        {
            if (!Board.PositionExists(position))
            {
                throw new ChessException($"Choose a valid board position!");
            }

            if (Board.GetPieceAt(position) == null)
            {
                throw new ChessException($"There's no piece at this origin position: {position}");
            }

            if (CurrentPlayer != Board.GetPieceAt(position).Color)
            {
                throw new ChessException($"Origin piece it's not yours!");
            }

            if (!Board.GetPieceAt(position).PossibleMovementsExists())
            {
                throw new ChessException($"There is no possible movements for choose origin piece!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPieceAt(origin).CanMoveTo(destiny))
            {
                throw new ChessException($"Invalid destiny position: {destiny}");
            }
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
