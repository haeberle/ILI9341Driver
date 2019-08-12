using System;
using System.Text;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        public void DrawRect(int left, int right, int top, int bottom, byte r, byte g, byte b)
        {
            lock (this)
            {
                SetWindow(left, right, top, bottom);

                var buffer = new byte[Width * 3];
                _spi.ConnectionSettings.DataBitLength = 23;
                if (r != 0 || g != 0 || b != 0)
                {
                    for (var i = 0; i < Width * 3; i = i + 3)
                    {
                        buffer[i] = r;
                        buffer[i + 1] = g;
                        buffer[i + 2] = b;
                    }
                }

                for (int y = 0; y < Height; y++)
                {
                    SendData(buffer);
                }
                _spi.ConnectionSettings.DataBitLength = 8;
            }
        }

        public void DrawRect(int left, int right, int top, int bottom, Color666 color)
        {
            var b = (byte)((UInt32)color & 0xff);
            var g = (byte)(((UInt32)color >> 8) & 0xff);
            var r = (byte)(((UInt32)color >> 16) & 0xff);

            DrawRect(left, right, top, bottom, r, g,b);

            //lock (this)
            //{
            //    SetWindow(left, right, top, bottom);

            //    var buffer = new byte[Width * 3];
            //    _spi.ConnectionSettings.DataBitLength = 23;

            //    var b = (byte)((UInt32)color & 0xff);
            //    var g = (byte)(((UInt32)color >> 8) & 0xff);
            //    var r = (byte)(((UInt32)color >> 16) & 0xff);

            //    if (r != 0 || g != 0 || b != 0)
            //    {
            //        for (var i = 0; i < Width * 3; i = i + 3)
            //        {
            //            buffer[i] = r;
            //            buffer[i + 1] = g;
            //            buffer[i + 2] = b;
            //        }
            //    }

            //    for (int y = 0; y < Height; y++)
            //    {
            //        SendData(buffer);
            //    }
            //    _spi.ConnectionSettings.DataBitLength = 8;
            //}
        }

        public void FillScreen(Color666 color)
        {
            lock (this)
            {
                DrawRect(0, Width - 1, 0, Height - 1, color);
            }
        }

        public void ClearScreen()
        {
            lock (this)
            {
                FillScreen(Color666.Black);
            }
        }
    }
}
