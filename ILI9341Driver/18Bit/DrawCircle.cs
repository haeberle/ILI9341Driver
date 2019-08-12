using System;
using System.Text;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        public void DrawCircle(UInt16 x0, UInt16 y0, UInt16 radius, Color666 color)
        {
            for (int y = -radius; y <= radius; y++)
                for (int x = -radius; x <= radius; x++)
                    if (x * x + y * y <= radius * radius)
                        DrawPixel((UInt16)(x + x0), (UInt16)(y + y0), color);

        }
    }
}
