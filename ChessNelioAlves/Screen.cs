﻿
using Entities;
using System;
using System.Data;

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
                    if (piece != null)
                    {
                        RenderPiece(piece);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H ");
        }

        public static void RenderPiece(Piece piece)
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

