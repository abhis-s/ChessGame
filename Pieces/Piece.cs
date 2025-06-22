using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChessGame.Pieces
{
    public abstract class Piece
    {
        protected ChessColor color;
        protected bool hasBeenMoved = false;

        public ChessColor Color
        {
            get { return color; }
            set { color = value; }
        }

        protected bool checkAndAddField(Board board, Coordinate checkPos, List<Coordinate> moves)
        {
            if (!checkPos.IsValid()) return false;

            Piece target = board.fields[checkPos.X, checkPos.Y];
            if (target == null)
            {
                moves.Add(checkPos);
                return true;
            }

            if (target.Color != Color)
            {
                moves.Add(checkPos);
            }

            return false;
        }

        protected void addBeam(Board board, Coordinate start, Coordinate direction, List<Coordinate> moves)
        {
            Coordinate pos = start + direction;

            while (checkAndAddField(board, pos, moves))
            {
                pos += direction;
            }
        }

        public abstract Coordinate[] PossibleMoves(Board board, Coordinate currentPos);
    }
}
