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
                board.InsertPieceAt(new Pawn(board, Color.Green), new Position(0, 0));
                board.InsertPieceAt(new Queen(board, Color.Black), new Position(3, 6));
                board.InsertPieceAt(new King(board, Color.Blue), new Position(2, 3));
                board.InsertPieceAt(new Knight(board, Color.White), new Position(5, 4));

                Screen.RenderBoard(board);
            }
            catch(BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadLine();
        }
    }
}
