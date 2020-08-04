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
                BoardPosition boardPositionA1 = new BoardPosition('a', 1);
                BoardPosition boardPositionC7 = new BoardPosition('c', 7);

                Console.WriteLine(boardPositionA1);
                Console.WriteLine(boardPositionA1.ToPosition());
                Console.WriteLine(boardPositionC7);
                Console.WriteLine(boardPositionC7.ToPosition());

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
