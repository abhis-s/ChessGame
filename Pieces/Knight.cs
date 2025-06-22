using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class Knight : Piece
    {
        public Knight(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();

            Coordinate[] directions = new Coordinate[]
            {
                new Coordinate(2, 1), new Coordinate(2, -1),
                new Coordinate(-2, 1), new Coordinate(-2, -1),
                new Coordinate(1, 2), new Coordinate(1, -2),
                new Coordinate(-1, 2), new Coordinate(-1, -2)
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
