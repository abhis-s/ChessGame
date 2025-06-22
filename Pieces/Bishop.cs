using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();
            
            addBeam(board, currentPos, new Coordinate(1, 1), moves); // up-right
            addBeam(board, currentPos, new Coordinate(1, -1), moves); // up-left
            addBeam(board, currentPos, new Coordinate(-1, 1), moves); // down-right
            addBeam(board, currentPos, new Coordinate(-1, -1), moves); // down-left
            return moves.ToArray();
        }
    }
}
