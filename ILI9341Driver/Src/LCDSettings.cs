using System;
using System.Text;

namespace ILI9341Driver
{
    public abstract class LCDSettings
    {

        public LCDSettings(int height, int width)
        {
            Width = width;
            Height = height;
        }

        private int _width;
        public int Width
        {
            get
            {
                return _width;
            }
            private set
            {
                _width = value;
            }
        }

        private int _height;

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        private byte _orientation;

        public byte Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
            }
        }

        //public abstract uint ColorCodeConverter(Color color);
    }

    public class M5StackLCDSettings : LCDSettings
    {
        public M5StackLCDSettings() : base (240, 320)
        {

        }

        //public override uint ColorCodeConverter(Color color)
        //{
        //    UInt16 rgb = (UInt16)color;

        //    int bits = (((rgb >> 19) & 0x1f) << 11) | (((rgb >> 10) & 0x3f) << 6) | (((rgb >> 3) & 0x1f));

        //    return (uint)bits;
        //}

        public const byte Portrait = 0xA8;     // 10101000 for M5Stack, was 0x48 = 01001000 for STM32F429I_DISCOVERY
        public const byte Landscape = 0x08;    // 00001000 for M5Stack, was 0xE8 = 11101000 for STM32F429I_DISCOVERY
        public const byte Portrait180 = 0x68;  // 01101000 for M5Stack, was 0x88 = 10001000 for STM32F429I_DISCOVERY
        public const byte Landscape180 = 0xC8;  // 11001000 for M5Stack, was 0x28 = 00101000 for STM32F429I_DISCOVERY   
    }
}
