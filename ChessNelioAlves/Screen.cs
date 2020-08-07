
using Entities;
using System;

namespace ChessNelioAlves
{
    public class Screen
    {
        public static void RenderBoard (Board board)
        {
            for(int row = 0; row < board.Rows; row++)
            {
                Console.Write($"{board.Rows - row} ");
                for (int col = 0; col < board.Columns; col++)
                {
                    Piece piece = board.GetPieceAt(row, col);
                    RenderPiece(piece);
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H ");
        }
        
        public static void RenderBoard (Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor darkCyan = ConsoleColor.DarkCyan;

            for(int row = 0; row < board.Rows; row++)
            {
                Console.Write($"{board.Rows - row} ");
                for (int col = 0; col < board.Columns; col++)
                {
                    if (possiblePositions[row, col])
                    {
                        Console.BackgroundColor = darkCyan;
                    }
                    else
                    {
                        Console.BackgroundColor = originalColor;
                    }

                    Piece piece = board.GetPieceAt(row, col);
                    RenderPiece(piece);
                    Console.BackgroundColor = originalColor;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H ");
            Console.BackgroundColor = originalColor;
        }

        public static void RenderPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else if (piece.Color == Color.Black)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(piece);
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = Enum.Parse<ConsoleColor>(piece.Color.ToString());
                    Console.Write(piece);
                    Console.ForegroundColor = consoleColor;
                }

                Console.Write(" ");
            }
        }

        public static BoardPosition ReadPosition()
        {
            string position = Console.ReadLine();
            char column = position[0];
            int row = int.Parse(position[1] + "");
            return new BoardPosition(column, row);
        }
    }
}

