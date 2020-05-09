using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    public class Axis
    {
        public Axis(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Axis(Axis a)
        {
            this.x = a.x;
            this.y = a.y;
        }
        public int x;
        public int y;
    }
}
