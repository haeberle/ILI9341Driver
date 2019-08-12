using System;
using System.Text;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        public void DrawPixel(UInt16 x, UInt16 y, Color666 color)
        {
            lock (this)
            {
                var b = (byte)((UInt32)color & 0xff);
                var g = (byte)(((UInt32)color >> 8) & 0xff);
                var r = (byte)(((UInt32)color >> 16) & 0xff);

                SetWindow(x, x, y, y);
                SendData(b,g,r);
            }
        }
    }
}
