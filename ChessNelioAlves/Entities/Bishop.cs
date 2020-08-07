
namespace Entities
{
    public class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] PossibleMovements()
        {
            throw new System.NotImplementedException();
        }
    }
}
