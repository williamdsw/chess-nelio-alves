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
                    Screen.RenderMatch(chessMatch);

                    Console.Write("Do you wish to pass the turn (y/n) ? ");
                    char pass = char.Parse(Console.ReadLine());
                    if (pass == 'y')
                    {
                        chessMatch.PassTurn();
                    }
                    else
                    {
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

            Console.Clear();
            Screen.RenderMatch(chessMatch);

            Console.ReadLine();
        }
    }
}
