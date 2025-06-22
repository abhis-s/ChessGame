using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class Castle : Piece
    {
        public Castle(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            return new Coordinate[0];
        }
    }
}
