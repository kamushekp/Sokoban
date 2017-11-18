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
            X = loc.X;
            Y = loc.Y;
        }

        public int CompareTo(object obj)
        {
            var loc = (Location)obj;
            return loc.X == X && loc.Y == Y ? 0 : -1;
        }
    }
}
