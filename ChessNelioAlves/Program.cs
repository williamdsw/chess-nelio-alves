using Entities;
using Entities.Exceptions;
using System;

namespace ChessNelioAlves
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch chessMatch = new ChessMatch();

            while (!chessMatch.EndMatch)
            {
                try
                {
                    Console.Clear();
                    Screen.RenderBoard(chessMatch.Board);
                    Console.WriteLine();
                    Console.WriteLine($"Turn: {chessMatch.Turn}");
                    Console.WriteLine($"Waiting for player {chessMatch.CurrentPlayer}");

                    Console.WriteLine();
                    Console.Write("Input origin position: ");
                    Position origin = Screen.ReadPosition().ToPosition();

                    chessMatch.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = chessMatch.Board.GetPieceAt(origin).PossibleMovements();

                    Console.Clear();
                    Screen.RenderBoard(chessMatch.Board, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Input destiny position: ");
                    Position destiny = Screen.ReadPosition().ToPosition();

                    chessMatch.ValidateDestinyPosition(origin, destiny);

                    chessMatch.PerformMove(origin, destiny);
                }
                catch (ChessException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.ReadLine();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            Console.ReadLine();
        }
    }
}
