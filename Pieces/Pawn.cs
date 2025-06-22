using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(ChessColor color)
        {
            this.color = color;
        }

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> moves = new List<Coordinate>();

            int direction = (this.Color == ChessColor.White) ? 1 : -1;
            int startRow = (this.Color == ChessColor.White) ? 1 : 6;

            // 1 forward (always)
            Coordinate oneStep = new Coordinate(currentPos.X, currentPos.Y + direction);
            if (oneStep.IsValid() && board.fields[oneStep.X, oneStep.Y] == null)
            {
                moves.Add(oneStep);

                // 2 forward (start)
                Coordinate twoStep = new Coordinate(currentPos.X, currentPos.Y + 2 * direction);
                if (currentPos.Y == startRow && board.fields[twoStep.X, twoStep.Y] == null)
                {
                    moves.Add(twoStep);
                }
            }

            // Diagonal (captures)
            Coordinate[] diagonals = new Coordinate[]
            {
                new Coordinate(currentPos.X + 1, currentPos.Y + direction),
                new Coordinate(currentPos.X - 1, currentPos.Y + direction)
            };

            foreach (Coordinate target in diagonals)
            {
                if (target.IsValid())
                {
                    Piece targetPiece = board.fields[target.X, target.Y];
                    if (targetPiece != null && targetPiece.Color != this.Color)
                    {
                        moves.Add(target);
                    }
                }
            }

            return moves.ToArray();
            //return new Coordinate[0];
        }
    }
}
