using System;

namespace ILI9341Driver._18Bit
{
    public class ColorConverter
    {
        public static Color666 ToRgb666(byte r, byte g, byte b)
        {
            var col = (Color666)(((r >> 2) << 12) | ((g >> 2) >> 6) | (b >> 2));
            return col;
        }

        public static byte[] ToRgb666Bytes(byte r, byte g, byte b)
        {
            var col = new byte[] {(byte)(r >> 2), (byte)(g >> 2), (byte)(b >> 2)};
            return col;
        }

        public static byte[] ToRgb666Bytes(int colorCode)
        {
            var b = (byte)((UInt32)colorCode & 0xff);
            var g = (byte)(((UInt32)colorCode >> 8) & 0xff);
            var r = (byte)(((UInt32)colorCode >> 16) & 0xff);

            var col = new byte[] { (byte)(r >> 2), (byte)(g >> 2), (byte)(b >> 2) };
            return col;
        }
    }

    public enum Color666 : UInt32
    {
        White = 0xFCFCFC,
        Silver = 0xC0C0C0,
        Gray = 0x808080,
        Black = 0x0,
        Red = 0xFC0000,
        Maroon = 0x800000,
        Yellow = 0xFCFC00,
        Olive = 0x808000,
        Lime = 0xFC00,
        Green = 0x8000,
        Aqua = 0xFCFC,
        Teal = 0x8080,
        Blue = 0xFC,
        Navy = 0x80,
        Fuchsia = 0xFC00FC,
        Purple = 0x800080,
    }

}
