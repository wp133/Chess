using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public class Rep
    {
        public bool HasM;
        public Position p1;
        public Position p2;
        public Rep(bool x, Position y, Position xx) 
        {
            HasM = x;
            p1 = y;
            p2 = xx;
        }
    }
}
