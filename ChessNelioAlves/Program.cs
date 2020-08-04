using Entities;
using Entities.Exceptions;
using System;

namespace ChessNelioAlves
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);
                board.InsertPieceAt(new Rook(board, Color.Black), new Position(0, 0));
                //board.InsertPieceAt(new Rook(board, Color.Black), new Position(0, 0));
                board.InsertPieceAt(new Queen(board, Color.Blue), new Position(1, 3));
                board.InsertPieceAt(new King(board, Color.Yellow), new Position(2, 4));
                //board.InsertPieceAt(new King(board, Color.Yellow), new Position(2, 10));

                Screen.RenderBoard(board);
                Console.ReadLine();
            }
            catch(BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }
    }
}
