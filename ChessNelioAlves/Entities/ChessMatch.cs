
using Entities.Exceptions;
using System.Collections.Generic;

namespace Entities
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool EndMatch { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            // Default values
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            EndMatch = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();

            InsertPieces();
        }

        public void ExecuteMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePieceAt(origin);
            piece.IncrementNumberOfMovements();
            Piece otherPiece = Board.RemovePieceAt(destiny);
            Board.InsertPieceAt(piece, destiny);

            if (otherPiece != null)
            {
                capturedPieces.Add(otherPiece);
            }
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

        public void InsertNewPiece (char column, int row, Piece piece)
        {
            BoardPosition boardPosition = new BoardPosition(column, row);
            Board.InsertPieceAt(piece, boardPosition.ToPosition());
            pieces.Add(piece);
        }

        private void InsertPieces ()
        {
            // White pieces
            InsertNewPiece('c', 1, new Rook(Board, Color.White));
            InsertNewPiece('c', 2, new Rook(Board, Color.White));
            InsertNewPiece('d', 2, new Rook(Board, Color.White));
            InsertNewPiece('e', 2, new Rook(Board, Color.White));
            InsertNewPiece('e', 1, new Rook(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));
            
            // Black Pieces
            InsertNewPiece('c', 7, new Rook(Board, Color.Black));
            InsertNewPiece('c', 8, new Rook(Board, Color.Black));
            InsertNewPiece('d', 7, new Rook(Board, Color.Black));
            InsertNewPiece('e', 7, new Rook(Board, Color.Black));
            InsertNewPiece('e', 8, new Rook(Board, Color.Black));
            InsertNewPiece('d', 8, new King(Board, Color.Black));
        }

        public HashSet<Piece> GetCapturedPiecesByColor (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in capturedPieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }

            return aux;
        }

        public HashSet<Piece> GetInGamePiecesByColor (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in capturedPieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }

            aux.ExceptWith(GetCapturedPiecesByColor(color));

            return aux;
        }
    }
}
