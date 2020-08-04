
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
                for (int col = 0; col < board.Columns; col++)
                {
                    Piece piece = board.GetPieceAt(row, col);
                    if (piece != null)
                    {
                        Console.Write($"{piece} ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

