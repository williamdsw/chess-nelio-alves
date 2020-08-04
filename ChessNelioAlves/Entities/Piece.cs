﻿
namespace Entities
{
    public class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece() { }
        public Piece(Position position, Board board, Color color)
        {
            Position = position;
            Board = board;
            Color = color;
            NumberOfMovements = 0;
        }
    }
}
