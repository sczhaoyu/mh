using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.mhxy
{
    class EventMsg
    {
        public IntPtr hwnd;
        public int mX;
        public int mY;
        public int posX;
        public int posY;
        public EventMsg(IntPtr hwnd, int mx, int my, int posx, int posy)
        {
            this.hwnd = hwnd;
            this.mX = mx;
            this.mY = my;
            this.posX = posx;
            this.posY = posy;
        }
    }
}
