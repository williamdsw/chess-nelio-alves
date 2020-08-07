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
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.EndMatch)
                {
                    Console.Clear();
                    Screen.RenderBoard(chessMatch.Board);

                    Console.WriteLine();
                    Console.Write("Input origin position: ");
                    Position origin = Screen.ReadPosition().ToPosition();

                    bool[,] possiblePositions = chessMatch.Board.GetPieceAt(origin).PossibleMovements();

                    Console.Clear();
                    Screen.RenderBoard(chessMatch.Board, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Input destiny position: ");
                    Position destiny = Screen.ReadPosition().ToPosition();

                    chessMatch.ExecuteMovement(origin, destiny);
                }
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
