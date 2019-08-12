using System;

namespace ILI9341Driver
{
    //[Flags]
    public enum Color565 : UInt16
    {
        White = 0xFFFF,
        Silver = 0xC618,
        Gray = 0x8410,
        Black = 0x0,
        Red = 0xF800,
        Maroon = 0x8000,
        Yellow = 0xFFE0,
        Olive = 0x8400,
        Lime = 0x7E0,
        Green = 0x400,
        Aqua = 0x7FF,
        Teal = 0x410,
        Blue = 0x1F,
        Navy = 0x10,
        Fuchsia = 0xF81F,
        Purple = 0x8010,




        //Black = 0x0000,
        //Blue = 0x001F,
        //LightBlue = 0xA53F,
        //DarkBlue = 0x421C,
        //Green = 0x07E0,
        //Red = 0xF800,
        //White = 0xFFFF

    }
    public static class ColorConverter
    {
        public static ushort ToRgb565(byte r, byte g, byte b)
        {
            return (ushort)((r << 11) | (g << 5) | b);
        }

        public static ushort ToRgb565(int rgb888)
        {
            //UInt16 rgb = (UInt16)rgb888;

            int bits = (((rgb888 >> 19) & 0x1f) << 11) | (((rgb888 >> 10) & 0x3f) << 6) | (((rgb888 >> 3) & 0x1f));

            return (ushort)bits;
        }
    }

    //[Flags]
    //public enum Color888
    //{
    //    Black = 0x000000,
    //    Blue = 0x0000FF,
    //    NanoBlue = 0x00AEEF,
    //    LightBlue = 0xA5A5FF,
    //    DarkBlue = 0x4342E6,
    //    Green = 0x00FF00,
    //    Red = 0xFF0000,
    //    White = 0xFFFFFF
    //}

    //public static class ColorConversion
    //{
    //    //public static Color565 ToRgb565(this Color888 color)
    //    //{
    //    //    UInt16 rgb = (UInt16)color;

    //    //    int bits = (((rgb >> 19) & 0x1f) << 11) | (((rgb >> 10) & 0x3f) << 6) | (((rgb >> 3) & 0x1f));

    //    //    return (Color565)bits;
    //    //}

    //    public static uint ToRgb565(this Color color)
    //    {
    //        UInt16 rgb = (UInt16)color;

    //        int bits = (((rgb >> 19) & 0x1f) << 11) | (((rgb >> 10) & 0x3f) << 6) | (((rgb >> 3) & 0x1f));

    //        return (uint)bits;
    //    }

    //}

    //[Flags]
    //public enum Color565 : UInt16
    //{
    //    Black = 0x0000,
    //    Blue = 0x001F,
    //    LightBlue = 0xA53F,
    //    DarkBlue = 0x421C,
    //    Green = 0x07E0,
    //    Red = 0xF800,
    //    White = 0xFFFF
    //}
}
