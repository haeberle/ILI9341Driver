using ILI9341Driver.Generic;
using System;
using System.Text;
using System.Threading;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18 : ILI9341Driver.Generic.ILI9341
    {
        public ILI9341Bit18(LCDSettings lcdSettings,
          GpioPin chipSelectPin = null,
          GpioPin dataCommandPin = null,
          GpioPin resetPin = null,
          GpioPin backlightPin = null,
          int spiClockFrequency = 18 * 1000 * 1000,
          SpiMode spiMode = SpiMode.Mode0,
          string spiBus = "SPI1") : base(lcdSettings, chipSelectPin, 
              dataCommandPin, resetPin, backlightPin, spiClockFrequency, spiMode, spiBus)
        {

        }

        public override void InitializeScreen()
        {
            lock (this)
            {
                WriteReset(GpioPinValue.Low);
                Thread.Sleep(10);
                WriteReset(GpioPinValue.High);
                SendCommand(Commands.SoftwareReset);
                Thread.Sleep(10);
                SendCommand(Commands.DisplayOff);

                SendCommand(Commands.MemoryAccessControl);
                SendData(8 | 0x40);

                SendCommand(Commands.PixelFormatSet);
                SendData(0x66);//18-bits per pixel

                SendCommand(Commands.FrameControlNormal);
                SendData(0x00, 0x1B);

                SendCommand(Commands.GammaSet);
                SendData(0x01);

                SendCommand(Commands.ColumnAddressSet); //width of the screen
                SendData(0x00, 0x00, 0x00, 0xEF);

                SendCommand(Commands.PageAddressSet); //height of the screen
                SendData(0x00, 0x00, 0x01, 0x3F);

                SendCommand(Commands.EntryModeSet);
                SendData(0x07);

                SendCommand(Commands.DisplayFunctionControl);
                SendData(0x0A, 0x82, 0x27, 0x00);

                SendCommand(Commands.SleepOut);
                Thread.Sleep(120);

                SendCommand(Commands.DisplayOn);
                Thread.Sleep(100);

                SendCommand(Commands.MemoryWrite);
            }
        }
    }
}
