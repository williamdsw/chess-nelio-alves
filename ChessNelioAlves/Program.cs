using Entities;
using System;

namespace ChessNelioAlves
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.InsertPieceAt(new Rook(board, Color.Black), new Position(0, 0));
            board.InsertPieceAt(new Queen(board, Color.Blue), new Position(1, 3));
            board.InsertPieceAt(new King(board, Color.Yellow), new Position(2, 4));

            Screen.RenderBoard(board);
            Console.ReadLine();
        }
    }
}
