using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    internal class Rook : Piece
    {
        public Rook(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();

            addBeam(board, currentPos, new Coordinate(0, 1), moves); // up
            addBeam(board, currentPos, new Coordinate(0, -1), moves); // down
            addBeam(board, currentPos, new Coordinate(1, 0), moves); // right
            addBeam(board, currentPos, new Coordinate(-1, 0), moves); // left

            return moves.ToArray();
        }
    }
}
