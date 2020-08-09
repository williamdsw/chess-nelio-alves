
using Entities.Exceptions;
using System.Collections.Generic;

namespace Entities
{
    public class ChessMatch
    {
        // FIELDS

        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        // PROPERTIES

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool EndMatch { get; private set; }
        public bool IsInCheck { get; private set; }

        // CONSTRUCTOR

        public ChessMatch()
        {
            // Default values
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            EndMatch = false;
            IsInCheck = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();

            InsertPieces();
        }

        // FUNCTIONS

        public Piece ExecuteMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePieceAt(origin);
            piece.IncrementNumberOfMovements();
            Piece otherPiece = Board.RemovePieceAt(destiny);
            Board.InsertPieceAt(piece, destiny);

            if (otherPiece != null)
            {
                capturedPieces.Add(otherPiece);
            }

            return otherPiece;
        }

        public void PerformMove(Position origin, Position destiny)
        {
            Piece capturated = ExecuteMovement(origin, destiny);

            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturated);
                throw new ChessException("You cannot put yourself in check!");
            }

            if (IsKingInCheck(WhoIsTheEnemy(CurrentPlayer)))
            {
                IsInCheck = true;
            }
            else
            {
                IsInCheck = false;
            }

            if (TestCheckMate(WhoIsTheEnemy(CurrentPlayer)))
            {
                EndMatch = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public void PassTurn()
        {
            Turn++;
            ChangePlayer();
        }

        private void UndoMovement(Position origin, Position destiny, Piece capturated)
        {
            Piece piece = Board.RemovePieceAt(destiny);
            piece.DecrementNumberOfMovements();
            if (capturated != null)
            {
                Board.InsertPieceAt(capturated, destiny);
                capturedPieces.Remove(capturated);
            }

            Board.InsertPieceAt(piece, origin);
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

        public bool TestCheckMate(Color color)
        {
            if (!IsKingInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in GetInGamePiecesByColor(color))
            {
                bool[,] possibleMovements = piece.PossibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMovements[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturated = ExecuteMovement(origin, destiny);
                            bool isInCheck = IsKingInCheck(color);
                            UndoMovement(origin, destiny, capturated);

                            if (!isInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void InsertNewPiece(char column, int row, Piece piece)
        {
            BoardPosition boardPosition = new BoardPosition(column, row);
            Board.InsertPieceAt(piece, boardPosition.ToPosition());
            pieces.Add(piece);
        }

        private void InsertPieces()
        {
            /*
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
            InsertNewPiece('d', 8, new King(Board, Color.Black));*/
            
            // TEST CHECKMATE
            // White pieces
            InsertNewPiece('c', 1, new Rook(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));
            InsertNewPiece('h', 7, new Rook(Board, Color.White));

            // Black Pieces
            InsertNewPiece('a', 8, new King(Board, Color.Black));
            InsertNewPiece('b', 8, new Rook(Board, Color.Black));
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
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

        public HashSet<Piece> GetInGamePiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }

            aux.ExceptWith(GetCapturedPiecesByColor(color));

            return aux;
        }

        private Color WhoIsTheEnemy(Color color)
        {
            return (color == Color.White ? Color.Black : Color.White);
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece piece in GetInGamePiecesByColor(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }

            return null;
        }

        public bool IsKingInCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
            {
                throw new ChessException($"There is no King of the {color} color in the Board!");
            }

            HashSet<Piece> enemies = GetInGamePiecesByColor(WhoIsTheEnemy(color));
            foreach (Piece enemy in enemies)
            {
                bool[,] possibleMovements = enemy.PossibleMovements();
                if (possibleMovements[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
