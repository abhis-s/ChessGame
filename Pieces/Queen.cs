using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class Queen : Piece
    {
        public Queen(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();

            // Bishop moves
            addBeam(board, currentPos, new Coordinate(1, 1), moves); // up-right
            addBeam(board, currentPos, new Coordinate(1, -1), moves); // up-left
            addBeam(board, currentPos, new Coordinate(-1, 1), moves); // down-right
            addBeam(board, currentPos, new Coordinate(-1, -1), moves); // down-left

            // Rook moves
            addBeam(board, currentPos, new Coordinate(0, 1), moves); // up
            addBeam(board, currentPos, new Coordinate(0, -1), moves); // down
            addBeam(board, currentPos, new Coordinate(1, 0), moves); // right
            addBeam(board, currentPos, new Coordinate(-1, 0), moves); // left

            return moves.ToArray();
        }
    }
}
