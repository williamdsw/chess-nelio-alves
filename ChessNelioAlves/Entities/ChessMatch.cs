
using Entities.Exceptions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Entities
{
    public class ChessMatch
    {
        // FIELDS

        private HashSet<Piece> pieces;
        private HashSet<Piece> capturatedPieces;
        
        // PROPERTIES

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool EndMatch { get; private set; }
        public bool IsInCheck { get; private set; }
        public Piece VunerableEnPassant { get; private set; }

        // CONSTRUCTOR

        public ChessMatch()
        {
            // Default values
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            EndMatch = false;
            IsInCheck = false;
            VunerableEnPassant = null;
            pieces = new HashSet<Piece>();
            capturatedPieces = new HashSet<Piece>();

            InsertPieces();
        }

        // FUNCTIONS

        public Piece ExecuteMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePieceAt(origin);
            piece.IncrementNumberOfMovements();
            Piece capturared = Board.RemovePieceAt(destiny);
            Board.InsertPieceAt(piece, destiny);

            if (capturared != null)
            {
                capturatedPieces.Add(capturared);
            }

            // #specialmove Little Rock
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePieceAt(rookOrigin);
                rook.IncrementNumberOfMovements();
                Board.InsertPieceAt(rook, rookDestiny);
            }

            // #specialmove Great Rock
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePieceAt(rookOrigin);
                rook.IncrementNumberOfMovements();
                Board.InsertPieceAt(rook, rookDestiny);
            }

            // #specialmove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturared == null)
                {
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destiny.Row - 1, destiny.Column);
                    }

                    capturared = Board.RemovePieceAt(pawnPosition);
                    capturatedPieces.Add(capturared);
                }
            }

            return capturared;
        }

        private void UndoMovement(Position origin, Position destiny, Piece capturated)
        {
            Piece piece = Board.RemovePieceAt(destiny);
            piece.DecrementNumberOfMovements();
            if (capturated != null)
            {
                Board.InsertPieceAt(capturated, destiny);
                capturatedPieces.Remove(capturated);
            }

            Board.InsertPieceAt(piece, origin);

            // #specialmove Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePieceAt(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.InsertPieceAt(rook, rookOrigin);
            }

            // #specialmove Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePieceAt(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.InsertPieceAt(rook, rookOrigin);
            }

            // #specialmove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturated == VunerableEnPassant)
                {
                    Piece pawn = Board.RemovePieceAt(destiny);
                    Position pawnPosition;
                    if (pawn.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destiny.Column);
                    }

                    Board.InsertPieceAt(pawn, pawnPosition);
                }
            }
        }

        public void PerformMove(Position origin, Position destiny)
        {
            Piece capturated = ExecuteMovement(origin, destiny);

            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturated);
                throw new ChessException("You cannot put yourself in check!");
            }

            Piece piece = Board.GetPieceAt(destiny);

            // #specialmove promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Row == 0) || 
                    (piece.Color == Color.Black && destiny.Row == 7))
                {
                    piece = Board.RemovePieceAt(destiny);
                    pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.InsertPieceAt(queen, destiny);
                    pieces.Add(queen);
                }
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

            // #specialmove En Passant
            if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VunerableEnPassant = piece;
            }
            else
            {
                VunerableEnPassant = null;
            }
        }

        public void PassTurn()
        {
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
            // White pieces
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));
            
            // Black pieces
            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in capturatedPieces)
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
