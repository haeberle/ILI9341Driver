using System;

namespace ILI9341Driver
{
    [Flags]
    public enum Color565 : UInt16
    {
        AliceBlue = 0xF7DF,
        AntiqueWhite = 0xFF5A,
        Aqua = 0x07FF,
        Aquamarine = 0x7FFA,
        Azure = 0xF7FF,
        Beige = 0xF7BB,
        Bisque = 0xFF38,
        Black = 0x0000,
        BlanchedAlmond = 0xFF59,
        Blue = 0x001F,
        BlueViolet = 0x895C,
        Brown = 0xA145,
        Burlywood = 0xDDD0,
        CadetBlue = 0x5CF4,
        Chartreuse = 0x7FE0,
        Chocolate = 0xD343,
        Coral = 0xFBEA,
        CornflowerBlue = 0x64BD,
        Cornsilk = 0xFFDB,
        Crimson = 0xD8A7,
        Cyan = 0x07FF,
        DarkBlue = 0x0011,
        DarkCyan = 0x0451,
        DarkGoldenrod = 0xBC21,
        DarkGray = 0xAD55,
        DarkGreen = 0x0320,
        DarkKhaki = 0xBDAD,
        DarkMagenta = 0x8811,
        DarkOliveGreen = 0x5345,
        DarkOrange = 0xFC60,
        DarkOrchid = 0x9999,
        DarkRed = 0x8800,
        DarkSalmon = 0xECAF,
        DarkSeaGreen = 0x8DF1,
        DarkSlateBlue = 0x49F1,
        DarkSlateGray = 0x2A69,
        DarkTurquoise = 0x067A,
        DarkViolet = 0x901A,
        DeepPink = 0xF8B2,
        DeepSkyBlue = 0x05FF,
        DimGray = 0x6B4D,
        DodgerBlue = 0x1C9F,
        FireBrick = 0xB104,
        FloralWhite = 0xFFDE,
        ForestGreen = 0x2444,
        Fuchsia = 0xF81F,
        Gainsboro = 0xDEFB,
        GhostWhite = 0xFFDF,
        Gold = 0xFEA0,
        Goldenrod = 0xDD24,
        Gray = 0x8410,
        Green = 0x400,
        GreenYellow = 0xAFE5,
        HoneyDew = 0xF7FE,
        HotPink = 0xFB56,
        IndianRed = 0xCAEB,
        Indigo = 0x4810,
        Ivory = 0xFFFE,
        Khaki = 0xF731,
        Lavender = 0xE73F,
        LavenderBlush = 0xFF9E,
        LawnGreen = 0x7FE0,
        LemonChiffon = 0xFFD9,
        LightBlue = 0xAEDC,
        LightCoral = 0xF410,
        LightCyan = 0xE7FF,
        LightGoldenRodYellow = 0xFFDA,
        LightGray = 0xD69A,
        LightGreen = 0x9772,
        LightPink = 0xFDB8,
        LightSalmon = 0xFD0F,
        LightSeaGreen = 0x2595,
        LightSkyBlue = 0x867F,
        LightSlateGray = 0x7453,
        LightSteelBlue = 0xB63B,
        LightYellow = 0xFFFC,
        Lime = 0x07E0,
        LimeGreen = 0x3666,
        Linen = 0xFF9C,
        Magenta = 0xF81F,
        Maroon = 0x8000,
        MediumAquamarine = 0x6675,
        MediumBlue = 0x0019,
        MediumOrchid = 0xBABA,
        MediumPurple = 0x939B,
        MediumSeaGreen = 0x3D8E,
        MediumSlateBlue = 0x7B5D,
        MediumSpringGreen = 0x7D3,
        MediumTurquoise = 0x4E99,
        MediumVioletRed = 0xC0B0,
        MidnightBlue = 0x18CE,
        MintCream = 0xF7FF,
        Mistyrose = 0xFF3C,
        Moccasin = 0xFF36,
        NavajoWhite = 0xFEF5,
        Navy = 0x0010,
        Oldlace = 0xFFBC,
        Olive = 0x8400,
        OliveDrab = 0x6C64,
        Orange = 0xFD20,
        OrangeRed = 0xFA20,
        Orchid = 0xDB9A,
        PaleGoldenRod = 0xEF55,
        PaleGreen = 0x9FD3,
        PaleTurquoise = 0xAF7D,
        PaleVioletRed = 0xDB92,
        PapayaWhip = 0xFF7A,
        PeachPuff = 0xFED7,
        Peru = 0xCC27,
        Pink = 0xFE19,
        Plum = 0xDD1B,
        PowderBlue = 0xB71C,
        Purple = 0x8010,
        Red = 0xF800,
        RosyBrown = 0xBC71,
        RoyalBlue = 0x435C,
        SaddleBrown = 0x8A22,
        Salmon = 0xFC0E,
        SandyBrown = 0xF52C,
        SeaGreen = 0x2C4A,
        SeaShell = 0xFFBD,
        Sienna = 0xA285,
        Silver = 0xC618,
        SkyBlue = 0x867D,
        SlateBlue = 0x6AD9,
        SlateGray = 0x7412,
        Snow = 0xFFDF,
        SpringGreen = 0x07EF,
        SteelBlue = 0x4416,
        Tan = 0xD5B1,
        Teal = 0x0410,
        Thistle = 0xDDFB,
        Tomato = 0xFB08,
        Turquoise = 0x471A,
        Violet = 0xEC1D,
        Wheat = 0xF6F6,
        White = 0xFFFF,
        WhiteSmoke = 0xF7BE,
        Yellow = 0xFFE0,
        YellowGreen = 0x9E66
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
