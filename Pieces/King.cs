using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class King : Piece
    {
        public King(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();

            Coordinate[] directions = new Coordinate[]
            {
                new Coordinate(1, 0),   // right
                new Coordinate(-1, 0),  // left
                new Coordinate(0, 1),   // up
                new Coordinate(0, -1),  // down
                new Coordinate(1, 1),   // up-right
                new Coordinate(-1, 1),  // up-left
                new Coordinate(1, -1),  // down-right
                new Coordinate(-1, -1)  // down-left
            };

            foreach (Coordinate dir in directions)
            {
                Coordinate target = currentPos + dir;
                checkAndAddField(board, target, moves);
            }

            return moves.ToArray();
        }
    }
}
