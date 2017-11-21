using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public class Location: IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Location(Location loc)
        {
            if (loc == null) throw new ArgumentNullException("null location", nameof(loc));

            X = loc.X;
            Y = loc.Y;
        }

        public Location Sub(Location loc)
        {
            if (loc == null) throw new ArgumentNullException("null location", nameof(loc));

            var sub = new Location(X - loc.X, Y - loc.Y);
            return sub;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException("null location", nameof(obj));

            var loc = (Location)obj;
            return loc.X == X && loc.Y == Y ? 0 : -1;
        }
    }
}
