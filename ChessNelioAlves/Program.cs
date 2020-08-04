using Entities;
using System;

namespace ChessNelioAlves
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Screen.RenderBoard(board);
            Console.ReadLine();
        }
    }
}
