using ILI9341Driver.Fonts;
using System;
using System.Text;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        public void DrawChar(int x, int y, FontCharacter character, Color666 color, Color666 background = Color666.Black, bool isDebug = false)
        {
            lock (this)
            {
                SetWindow(x, x + character.Width - 1, y, y + character.Height - 1);

                var pixels = new byte[character.Width * character.Height*3];
                var pixelPosition = 0;

                var b = (byte)((UInt32)color & 0xff);
                var g = (byte)(((UInt32)color >> 8) & 0xff);
                var r = (byte)(((UInt32)color >> 16) & 0xff);

                var bb = (byte)((UInt32)background & 0xff);
                var bg = (byte)(((UInt32)background >> 8) & 0xff);
                var br = (byte)(((UInt32)background >> 16) & 0xff);

                for (var segmentIndex = 0; segmentIndex < character.Data.Length; segmentIndex++)
                {
                    var segment = character.Data[segmentIndex];
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x80) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x80) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x80) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x40) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x40) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x40) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x20) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x20) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x20) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x10) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x10) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x10) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x8) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x8) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x8) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x4) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x4) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x4) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x2) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x2) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x2) != 0 ? b : bb; pixelPosition++;
                    }
                    if (pixelPosition < pixels.Length)
                    {
                        pixels[pixelPosition] = (segment & 0x1) != 0 ? r : br; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x1) != 0 ? g : bg; pixelPosition++;
                        pixels[pixelPosition] = (segment & 0x1) != 0 ? b : bb; pixelPosition++;
                    }
                }

                if (isDebug)
                {
                    var currentBuffer = string.Empty;
                    for (var pixel = 0; pixel < pixels.Length; pixel++)
                    {
                        if (pixels[pixel] > 0)
                        {
                            currentBuffer += "X";
                        }
                        else
                        {
                            currentBuffer += "-";
                        }
                        if (currentBuffer.Length >= character.Width)
                        {
                            Console.WriteLine(currentBuffer);
                            currentBuffer = string.Empty;
                        }
                    }
                }

                SendData(pixels);
            }
        }
    }
}
