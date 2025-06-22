using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Coordinate
    {
        private int x;
        private int y;
        
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsValid()
        {
            return x >= 0 && x <= 7 && y >= 0 && y <= 7;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public char C
        {
            get { return (char)('A' + x); }
            set { x = char.ToUpper(value) - 'A'; }
        }

        public int Z
        {
            get { return 8 - y; }
            set { y = 8 - value; }
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }

        public static Coordinate operator -(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", C, Z);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Coordinate other)
                return this.X == other.X && this.Y == other.Y;
            return false;
        }

        public override int GetHashCode()
        {
            return X * 8 + Y;
        }
    }
}
