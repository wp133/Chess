using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public class Direction
    {
        public readonly static Direction N = new Direction(-1, 0);
        public readonly static Direction S = new Direction(1, 0);
        public readonly static Direction E = new Direction(0, 1);
        public readonly static Direction W = new Direction(0, -1);
        public readonly static Direction NE = N + E;
        public readonly static Direction NW = N + W;
        public readonly static Direction SE = S + E;
        public readonly static Direction SW = S + W;
        public int PlusR {get; }
        public int PlusC {get; }
        public Direction(int nig, int ger) 
        { 
            PlusR = nig;
            PlusC = ger;
        }
        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction(a.PlusR + b.PlusR, b.PlusC + a.PlusC);
        }
        public static Direction operator *(int skalar, Direction c)
        {
            return new Direction(skalar * c.PlusR, skalar * c.PlusC);
        }
    }
}
