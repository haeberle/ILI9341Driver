using System;
using System.Text;

namespace ILI9341Driver
{
    public partial class ILI9341
    {
        public void DrawPixel(UInt16 x, UInt16 y, Color565 color)
        {
            lock (this)
            {
                SetWindow(x, x, y, y);
                SendData((ushort)color);
            }
        }

        public void DrawPixel(UInt16 x, UInt16 y, Color888 color)
        {
            lock (this)
            {
                SetWindow(x, x, y, y);
                SendData((ushort)ColorConversion.ToRgb565(color));
            }
        }

        public void SetPixel(int x, int y, Color565 color)
        {
            lock (this)
            {
                SetWindow(x, x, y, y);
                SendData((ushort)color);
            }
        }
    }
}
